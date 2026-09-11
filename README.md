# registries-scanner

Scanner application to create accessible electronic versions of submitted paper forms.

`registries-scanner` is a Windows Forms desktop application (C#, .NET Framework 4.8) used to scan paper documents, read the barcode that identifies each document, look up the matching document record, and upload the resulting PDF back to the Document Record Service (DRS) API.

## Solution layout

| Project            | Type            | Responsibility |
|--------------------|-----------------|----------------|
| `RegScan`          | WinForms (.exe) | Main application: UI, scanning workflow, DRS API calls, PDF generation. Entry point is `Program.Main` launching `frmMDIMain`. |
| `AppConfiguration` | Class library   | Strongly-typed access to application settings via `ConfigKeys`, plus configuration-section encryption. |
| `BarCodeScanner`   | Class library   | `BarCodeImageScanner`, a bitmap-based barcode reader supporting Code39, EAN/UPC, and Code128. |
| `VSTTwain`         | Third-party SDK | VintaSoft TWAIN SDK assemblies and vendor samples used to acquire images from scanner hardware. See `VSTTwain/Documentation/readme.txt`. |

## Architecture overview

**UI layer** 

— `frmMDIMain` is the MDI parent window that manages the menu bar and child windows (`frmScannerDocument`, `frmBoxManagement`). 
- `frmScannerDocument` drives a scanning session: acquiring images, displaying them, and building a `DocumentObj`.

**Scanning** 

- Image acquisition uses the VintaSoft TWAIN SDK (`Vintasoft.Twain`). 
  - The SDK is registered at startup in the `frmMDIMain` constructor using the `TWAINSDK*` configuration keys.

**Barcode reading** 

— `BarCodeObj.ScanForBarcode` wraps `BarCodeScanner.BarCodeImageScanner` to detect the document barcode (currently Code39, scanned vertically) from a `Bitmap`.

**Document model** 

— `DocumentObj` mirrors the DRS API `documentSummary` schema as closely as possible.

**API layer** 

— `DocumentApi` is the controller for DRS API calls: `SearchByBarcode`, `GetSearch`, `UpdateDocumentRecord`, and `UploadDocument`. 
- Requests are issued through `APIRequest` (RestSharp).

**Utilities** 

— `UtilityObj` provides logging (`WriteLog`) and value/format conversion helpers.

## Prerequisites

- Windows with a TWAIN-compatible scanner and driver.
- Visual Studio 2022 with the .NET Framework 4.8 developer pack.
- A valid VintaSoft TWAIN SDK license (username, email, and key).
- Access to the DRS API and its API key.

## Configuration

Settings are read from the application's `appSettings` and surfaced through `AppConfiguration.ConfigKeys`. At startup, `Program.Main` initializes `ConfigKeys` and then encrypts the `appSettings` section (`Security.EncryptExternalSection("appSettings")`), so sensitive values are stored encrypted at rest.

Keys read by `ConfigKeys` include:

| Key | Purpose |
|-----|---------|
| `ENV` | Environment name (`dev`/`test` enable console logging). |
| `API_URL`, `APIKEY`, `ACCOUNT_ID` | DRS API endpoint and credentials. |
| `AUTH_SVC_URL`, `AUTH_TIMEOUT`, `CLIENT_ID`, `CLIENT_ACCOUNT` | Authentication service settings. |
| `TWAINSDKUSERNAME`, `TWAINSDKEMAIL`, `TWAINSDKKEY` | VintaSoft TWAIN SDK registration. |
| `LOGPATH` | Log file location used by `UtilityObj.WriteLog`. |

## Build and run

1. Open the solution in Visual Studio 2022.
2. Restore NuGet packages (RestSharp, Newtonsoft.Json, PdfSharp, VintaSoft TWAIN SDK).
3. Provide the required configuration values (including the TWAIN SDK license and DRS API key).
4. Build the solution and run the `RegScan` project.

## External integrations

- **VintaSoft TWAIN SDK** 
  — scanner image acquisition. Vendor docs: http://www.vintasoft.com/docs/vstwain-dotnet/
- **DRS API** 
  - document search, metadata update, and PDF upload (`DocumentApi`).
- **PdfSharp** 
  - PDF generation from scanned images.

## Known limitations / TODOs

The following items are marked as `TODO` in the source and remain outstanding:

- `DocumentApi.UpdateDocumentRecord` — update how information is passed back to the API, including verification and checking that the data should be changed.
- `DocumentApi.UploadDocument` — handle passing additional parameters via `param` (ticket #33043).

