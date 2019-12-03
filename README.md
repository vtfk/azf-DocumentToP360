# azf-DocumentToP360
Durable function orchestration service to P360

**OBS! Remember to create a local.settings.json file and add Archeo api key, Base URL for P360 and authorization key for P360:**

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ArcheoApiKey": "ARCHEOKEY",
    "BaseURL": "BASEURLP360",
    "a_Key": "AUTHP360"
  }
}
```
