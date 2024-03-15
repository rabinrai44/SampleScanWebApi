export class ParcelItem {
  poNumber: string = '';
  model: string = '';
  upc: string = '';
  quantity: number = 0;
  description: string = '';
  setupRequired: boolean = false;
  RLP: string = '';
  zone: string = '';
  trackingNumbers: TrackingNumber[] = [];
  existingRLP: boolean = false;
  damageQuantity: number = 0;
  shipmentId?: string;

  constructor(props?: Partial<ParcelItem>) {
    Object.assign(this, props);
  }
}

export class TrackingNumber {
  trackingNo: string = '';
  deliveryId: string = '';
  cartonId: string = '';
  isDamage: boolean;
  constructor(props?: Partial<TrackingNumber>) {
    Object.assign(this, props);
  }
}

export interface TrackingResponse {
  recordExists: boolean;
}


export class VendorInfo {
  vendorId: string;
  vendorName: string;
  buyerEmail: string;
  buyerName: string;
  carrierPreference: string;
  dateCreated: string;
  dateLastChanged: string;
  featuresP: string;
  allowShipmentIdReceiving: boolean;
  useCartonIdReceiving: boolean;
  serviceLevelP: string;
  userCreated: string;
  userLastChanged: string;
  vendorDescription: string;

  constructor(props?: Partial<VendorInfo>) {
    Object.assign(this, props);
  }
}
