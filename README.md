# MS CSCP Education task

## ProjectTasksApi

Service is available at [projecttasksapilinuxappservice.azurewebsites.net](https://projecttasksapilinuxappservice.azurewebsites.net)

[Interactive swagger UI](https://projecttasksapilinuxappservice.azurewebsites.net/swagger/index.html)

> **Note:** Please set the `version` path parameter to `1` when interacting with API through Swagger UI.

## ProjectTasksCosmosApi

Service is available at [projecttaskscosmosapilinuxappservice.azurewebsites.net](https://projecttaskscosmosapilinuxappservice.azurewebsites.net)

[Interactive swagger UI](https://projecttaskscosmosapilinuxappservice.azurewebsites.net/swagger/index.html)

> **Note:** Please set the `version` path parameter to `1` when interacting with API through Swagger UI.

## ProjectTasksFrontend

Site is available at [brave-ground-025ca3103.2.azurestaticapps.net/](https://brave-ground-025ca3103.2.azurestaticapps.net/)

Readonly version of site (used `Cosmos` backend) is available at [orange-moss-00a009110.2.azurestaticapps.net](https://orange-moss-00a009110.2.azurestaticapps.net/)

## ProjectTasksFunction

Azure Function based on Timer trigger.

Function runs by CRON expression: `*/30 * * * * *` (each 30 seconds).

> **Note**: Please note that there might be non-significant delays in data update periods.
> It's due to the serverless compute model used in underlying databases
