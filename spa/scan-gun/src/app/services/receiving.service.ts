import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { TrackingResponse } from '../models/receiving.model';

@Injectable({
  providedIn: 'root',
})
export class ReceivingService {
  private baseUrl = 'http://localhost:8080/api/receiving';

  constructor(private http: HttpClient) {}

  validatePOSID(poNumber: string) {
    return this.http.get<any>(`${this.baseUrl}/parcel-item/${poNumber}`);
  }

  retrieveCodeValues(
    codeType: string,
    code1: string,
    code2: string,
    lbrCommand: string
  ) {
    console.log('Mock: retrieveCodeValues');
    return this.http.post<any>(`${this.baseUrl}/code-values`, {
      codeType,
      code1,
      code2,
      lbrCommand,
    });
  }

  validateUPC(upc: string) {
    console.log('Mock: validateUPC');
    return of('Mock UPC');
  }

  validateDeliveryId(deliveryId: string) {
    console.log('Mock: validateDeliveryId');
    return of('Mock DeliveryId');
  }

  validateCartonId(cartonId: string) {
    console.log('Mock: validateCartonId');
    return of('Mock CartonId');
  }

  validateTrackingNo(trackingNo: string) {
    console.log('Mock: validateTrackingNo');
    return this.http.get<TrackingResponse>(
      `${this.baseUrl}/tracking/${trackingNo}`
    );
  }
}
