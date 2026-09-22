# AppConfiguration

Class library that centralizes access to the application's configuration for the `registries-scanner` solution. It is referenced by the `RegScan` application.

## Responsibilities

Provide strongly-typed, IntelliSense-friendly access to `appSettings` values through `ConfigKeys`, instead of scattering `ConfigurationManager.AppSettings["..."]` lookups throughout the codebase.

## ConfigKeys

Reads every known key from `ConfigurationManager.AppSettings` in its constructor and exposes each as a static property. Construct it once at startup (see `RegScan.Program.Main`) so the values are populated before first use.

## Configuration keys

| Property                                         | Purpose |
|--------------------------------------------------|---------|
| `ENV`                                            | Environment name (`dev`/`test` enable console logging in `RegScan`). |
| `API_URL`, `APIKEY`, `ACCOUNT_ID`                | DRS API endpoint and credentials. |
| `AUTH_SVC_URL`, `AUTH_TIMEOUT`, `CLIENT_ID`, `CLIENT_ACCOUNT` | Authentication service settings. (Currently not utilized but remain for potential future use.) |
| `TWAINSDKUSERNAME`, `TWAINSDKEMAIL`, `TWAINSDKKEY` | VintaSoft TWAIN SDK registration. |

## Notes

`ConfigKeys` reads from `appSettings`. In `RegScan`, `appSettings` is stored in an external file via `configSource` and encrypted at rest by `RegScan.Security.EncryptExternalSection`.
