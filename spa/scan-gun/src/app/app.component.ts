import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { OnInit } from '@angular/core';
import { HostListener } from '@angular/core';
import { ParcelItem, TrackingNumber, VendorInfo } from './models/receiving.model';
import { ReceivingService } from './services/receiving.service';
import { SoundService } from './services/sound.service';

const DEFAULT_FUNC_KEYS = [
  'F1',
  'F2',
  'F3',
  'F4',
  'F5',
  'F6',
  'F7',
  'F8',
  'F9',
  'F10',
  'Enter',
];

const SCAN_LABELS = {
  PO_SID: 'PO/Shipment Id',
  MODEL_UPC: 'Model/UPC',
  UNIT_CARTON: 'Unit/Carton',
  TRACKING_NO: 'Tracking Number',
  DELIVERY_ID: 'Delivery ID',
  CARTON_ID: 'Carton ID',
};

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit, AfterViewInit {
  title = 'Scan Gun';

  @ViewChild('scanInputField') scanInputField: ElementRef;
  navbarTitle = 'Parcel Item Receiving';
  trackList: TrackingNumber[] = [];
  scanLabel = SCAN_LABELS.PO_SID;
  itemInfo: ParcelItem;
  currentTrackingNo: string;
  parcelItem: ParcelItem;
  isDamage = false;
  currentParcelItemData: any;
  vendorInfo: VendorInfo;
  trackNumValid = false;
  poNo: string;
  applicationCode: string = "Parcel";
  workflowCode: string = "ParcelReceiving";

  constructor(
    private receivingService: ReceivingService,
    private soundService: SoundService
  ) {
    console.log('AppComponent constructor');
  }

  ngAfterViewInit(): void {
    console.log('AppComponent ngAfterViewInit');
    this.setActive(true);
  }

  ngOnInit(): void {
    console.log('AppComponent ngOnInit');
  }

  private setActive(active: boolean) {
    if (active) {
      this.scanInputField.nativeElement.focus();
    }
  }

  validatePOSID(poNumber: string, itemNo: string) {
    console.log('validatePOSID');

    // API call to validate PO/SID - retrieve codeValues/retrieve
    this.receivingService.retrieveCodeValues('PO', '', poNumber, this.workflowCode).subscribe({
      next: (data) => {
        // Check if user entered a valid PO number or Shipment ID
        const extractCodeValues = (values) => {
          return values.split('^')[1];
        };
        let poNum = poNumber;
        let shipmentId = '';
        const codeString2 = data.codeString2;
        if (codeString2.startsWith('SID^') && codeString2.length > 3) {
          // User entered a valid PO number and returns a Shipment ID that associates with the PO number
          this.currentParcelItemData.workflowBeginWithShipmentId = false;
          poNum = extractCodeValues(codeString2);
        } else if (codeString2.startsWith('PO^') && codeString2.length > 3) {
          // User entered a valid Shipment ID and returns a PO number that associates with the Shipment ID
          this.currentParcelItemData.workflowBeginWithShipmentId = true;
          shipmentId = extractCodeValues(codeString2);
        } else {
          this.currentParcelItemData.workflowBeginWithShipmentId = false;
        }

        // Update the current form data
        this.updateParcelItemData({poNum, shipmentId, itemNo});
        this.setScanValue('');
        // Success move next scan - Model UPC
        this.scanLabel = SCAN_LABELS.DELIVERY_ID;
      },
      error: (err) => {
        console.log('validatePOSID error', err);
        this.soundService.playError();
      },
    });
  }

  private updateParcelItemData({poNum, shipmentId, itemNo, trackingNumbers}: any) {
    this.currentParcelItemData.poNum = poNum;
    this.currentParcelItemData.shipmentId = shipmentId;
    this.currentParcelItemData.itemNo = itemNo;

    if (!trackingNumbers) {
      trackingNumbers = [];
    }
    this.parcelItem = new ParcelItem({
      poNumber: poNum,
      shipmentId: shipmentId,
      trackingNumbers,
    });

    this.updateCurrentFormDataState();
    this.updateParcelItemState();
  }

  validateTrackingNo(trackNum: string) {
    console.log('validateTrackingNo');

    this.receivingService.validateTrackingNo(trackNum).subscribe({
      next: (response) => {
        // Check if the tracking number is existing
        if (response.recordExists) {
          // throw error message
          console.log('Tracking number is existing');
          return;
        }
        // Update the current form data

        // Continue with the process
        this.soundService.playSelect();
        this.currentTrackingNo = trackNum;

        let hasTrack = false;
        this.trackList = this.parcelItem.trackingNumbers
          ? this.parcelItem.trackingNumbers
          : [];

        const trackItem = new TrackingNumber();
        trackItem.trackingNo = trackNum;
        if (this.isDamage) {
          trackItem.isDamage = true;
        }

        // Check if workflow begins with ShipmentId instead of PO number
        if (this.hasWorkflowBeginWithShipmentId()) {
          // Add shipmentId as deliveryId
          if (
            (this.vendorInfo.allowShipmentIdReceiving &&
              !this.vendorInfo.useCartonIdReceiving) ||
            (this.vendorInfo.allowShipmentIdReceiving &&
              this.vendorInfo.useCartonIdReceiving)
          ) {
            trackItem.deliveryId = this.parcelItem.shipmentId;
          }
          // add 'N/A' as cartonId
          if (
            this.vendorInfo.allowShipmentIdReceiving &&
            !this.vendorInfo.useCartonIdReceiving
          ) {
            trackItem.cartonId = 'N/A';
          }
        }

        // Check if the tracking number is already existing
        const index = this.trackList.findIndex(
          (item) => item.trackingNo === trackNum
        );
        if (index !== -1) {
          hasTrack = true;
          // update the existing tracking number
          this.trackList[index] = trackItem;
        } else {
          // add the tracking number to the list
          this.trackList.push(trackItem);
        }

        // update the parcel item with the new tracking number list
        this.parcelItem.trackingNumbers = this.trackList;
        if (this.parcelItem.trackingNumbers.length > 0) {
          this.trackNumValid = true;
        } else {
          this.trackNumValid = false;
        }

        if (!this.vendorInfo.useCartonIdReceiving || hasTrack) {
          this.currentParcelItemData.trackingNumbers = '';
        } else {
          // Success move next scan - Delivery ID
          this.scanLabel = SCAN_LABELS.DELIVERY_ID;
        }

        // Check DeliveryNo enable to collect or not - the user wil not propmt to scan the deliveryId
        if (
          this.scanLabel === SCAN_LABELS.DELIVERY_ID &&
          !this.vendorInfo.useCartonIdReceiving
        ) {
          this.scanLabel = SCAN_LABELS.CARTON_ID;
        }

        // Check if the user is allowed to scan the cartonId
        if (
          this.scanLabel === SCAN_LABELS.CARTON_ID &&
          !this.vendorInfo.useCartonIdReceiving
        ) {
          this.scanLabel = SCAN_LABELS.TRACKING_NO;
        }

        // Update the parcel item state in LocalStorage
        this.updateParcelItemState();
        this.updateCurrentFormDataState();
      },
      error: (err) => {
        console.log('validateTrackingNo error', err);
        this.soundService.playError();
      },
    });
  }

  private updateParcelItemState() {
    localStorage.setItem('currentParcelItem', JSON.stringify(this.parcelItem));
  }

  private updateCurrentFormDataState() {
    localStorage.setItem(
      'currentParcelItemForm',
      JSON.stringify(this.currentParcelItemData)
    );
  }

  private hasWorkflowBeginWithShipmentId() {
    const value = this.currentParcelItemData.workflowBeginWithShipmentId;
    console.log('Has workflow begin with ShipmentId?', value);
    return value;
  }

  validateDeliveryId() {
    console.log('validateDeliveryId');

    // Success move next scan - Carton ID
    this.scanLabel = SCAN_LABELS.CARTON_ID;
  }

  async validateModelUpc(model: string) {
    console.log('validateModelUpc');

    try {
      const response = await this.receivingService.retrieveCodeValues('UPC', model, this.poNo, 'ParcelItemReceiving').toPromise();
      console.log('validateModelUpc response', response);
      // Success move next scan - Tracking Number
      this.scanLabel = SCAN_LABELS.TRACKING_NO;

    } catch (error) {
      console.log('validateModelUpc error', error);
      this.soundService.playError();
  }

    // Success move next scan - Tracking Number
    this.scanLabel = SCAN_LABELS.TRACKING_NO;
  }

  validateCartonId(cartonId: string) {
    console.log('validateCartonId', cartonId);

    // Success and complete
    // stay on the same scan label - Carton ID
  }

  getScanValue() {
    return this.scanInputField.nativeElement.value;
  }

  setScanValue(value: string) {
    this.scanInputField.nativeElement.value = value;
  }

  onProcessScan() {
    console.log('onScanSubmit');
    const scanValue = this.getScanValue();
    this.soundService.playSelect();
    switch (this.scanLabel) {
      case SCAN_LABELS.PO_SID:
        this.validatePOSID(scanValue, '');
        this.setScanValue('');
        break;

      case SCAN_LABELS.TRACKING_NO:
        this.validateTrackingNo(scanValue);
        this.setScanValue('');
        break;

      case SCAN_LABELS.DELIVERY_ID:
        this.validateModelUpc(scanValue);
        this.setScanValue('');
        break;

      case SCAN_LABELS.CARTON_ID:
        this.validateCartonId(scanValue);
        this.setScanValue('');
        break;
    }
  }

  @HostListener('document:keydown', ['$event'])
  @HostListener('document:keyup', ['$event'])
  onKeyboardEvent(event: KeyboardEvent) {
    // disabled default behavior
    if (DEFAULT_FUNC_KEYS.includes(event.key)) {
      event.preventDefault();
    }

    if (event.type === 'keydown') {
      return;
    }

    switch (event.key) {
      case 'F1':
        console.log('F1 - GO TO MENU');
        break;

      case 'F5':
        console.log('F5 - CONFIRM');
        break;

      case 'Enter':
        console.log('Enter - SUBMIT');
        this.onProcessScan();
        break;
    }
  }
}
