// Vendors API
// GET /api/receiving/vendors/{vendorId} - Get the Vendor information


// Cartons API
// POST /api/receiving/cartons - Create a new cartonId records
// POST /api/receiving/cartons/{cartonId}/items - Create a new item record
// Schema for the cartonId record
// {
"cartonIdList": [
    {
        "vendorId": "string",
        "cartonId": "string",
        "deliveryId": "string",
        "poNumber": "string",
        "itemNo": "string",
        "cartonQty": 0,
        "trackingNumber": "string",
        "warehouseCode": 0,
        }
]
}

// Response : 204 No Content

// POST /api/receiving/cartons/{cartonId} - Validate the cartonId
// Schema for the item record
{
    "vendorId": "string",
    "warehouseCode": 0,
}

// Response : 204 No Content


// Delivery API
// POST /api/receiving/deliveries/{deliveryId} - Validate the deliveryId
// Schema for the deliveryId record
{
    "vendorId": "string"
}

// Response : 204 No Content


// Receipts API
// POST /api/receiving/receipts - Create a new receipt record
// Schema for the receipt record
{
    "vendorId": "string",
    "deliveryId": "string",
    "poNumber": "string",
    "itemNo": "string",
    "cartonId": "string",
    "receiptQty": 0,
    "receiptDate": "string",
    "receiptTime": "string",
    "warehouseCode": 0,
}

// Response : 204 No Content

// GET /api/receiving/receipts/{receiptId} - Get the receipt information


// PurchaseOrders API
// GET /api/receiving/purchaseOrders/{poNumber} - Get the purchase order information
// GET /api/receiving/purchaseOrders/{poNumber}/items/{itemNo} - Get the purchase order item details
// GET /api/receiving/purchaseOrders/{poNumber}/items/{itemNo}/cartons/{cartonId} - Get the purchase order carton details
// Item Details
{
    "poNumber": "string",
        "shipmentId": "string",
        "poDate": "string",
        "quantityOrdered": 0,
        "quantityReceived": 0,
            "itemNumber": "string",
}

// TrackingNumbers API
// GET /api/receiving/trackingNumbers/{trackingNumber} - Get the tracking number information
// GET /api/receiving/trackingNumbers/{trackingNumber}/cartons/{cartonId} - Get the tracking number carton details
// GET /api/receiving/trackingNumbers/{trackingNumber}/cartons/{cartonId}/items/{itemNo} - Get the tracking number item details
// POST /api/receiving/trackingNumbers/{trackingNumber}/validate - Validate the tracking number
// Schema for the tracking number
{
    "applicationMode": "Parcel",
    "lbrCommand": "ParcelReceiving",
}


// ReceivingLicensePlates (RLP) API
// POST /api/receiving/receivingLicensePlates/createFromPONumber/{poNumber} - Create a new RLP record - RLP-8283-2323
// schema for the RLP record
{
    "itemId": "string",
        "trackingNumber": "string",
        "receivedQty": 0,
        "unitesPerCarton": 0,
        "damagedQty": 0,
        "damagedReason": "string",
        "menuMode": "string",
}
