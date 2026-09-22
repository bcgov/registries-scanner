# BarCodeScanner

Class library that reads barcodes directly from bitmap images for the `registries-scanner` solution. It has no dependency on scanner hardware as it operates purely on `System.Drawing.Bitmap` input.

## Responsibilities

Detect and decode barcodes from a scanned page image.

## `BarCodeImageScanner` 

Static image-based barcode reader. Supports Code39, EAN/UPC, and Code128 (`BarcodeType`) and can scan vertically, horizontally, or both.

### Public Methods

- `FullScanPage(ref ArrayList codesRead, Bitmap bmp, int numscans)` 
  - scans the active frame both vertically and horizontally for `FullScanBarcodeTypes`.
- `ScanPage(ref ArrayList codesRead, Bitmap bmp, int numscans, ScanDirection direction, BarcodeType types)`
  - scans in a single direction for the requested barcode types.


## Public Properties 

- `FullScanBarcodeTypes`
  - If a more restricted list of barcodes is required it can be set using `m_FullScanBarcodeTypes`.
  - Currently there is no documentation on the barcode type used on documents
- `UseBarcodeZones` 
  - While `m_bUseBarcodeZones` is true this class will be able to detect multiple barcodes on a single line. 
  - Greatly impacts performance when true
- `numscans` 
  - controls how many passes are made over the page
    - 50–100 typically yields good results. 
    - For multi-page TIFFs, select the desired frame with `bmp.SelectActiveFrame(FrameDimension.Page, page)` before scanning.

## Usage

- User presses "Scan" button 
  - Triggers `frmScannerDocument.cs`s `btnScanPage_Click` 
    - Calls `setUpScanner` -> `SubscribeToDeviceEvents` 
      - Included in the device event list is `device_ScanFinished`
        - Calls `ProcessCompleted` calls `ProcessFirstPage` 
          - Calls `RegScan.BarCodeObj` method `ScanForBarcode` calls `Scan` 
            - Calls `BarCodeScanner`s method `ScanPage` with the following parameters
              - `numscans` = 100
              - Vertical scan
              - Barcode type: Code39
