```bash
resourceGroup="project_tasks-backend-api-rg" # The new one
location="westeurope"
keyVaultName="project-tasks-kv"

# Create a keyvault
az keyvault create --name "$keyVaultName" --resource-group $resourceGroup --location "$location"

# Create a secret
az keyvault secret set --vault-name "$keyVaultName" --name "AzureSqlConnection" --value "*************"

# Create and assign a managed identity
webApiName="ProjectTasksApiLinuxAppService"
az webapp identity assign --name "$webApiName" --resource-group $resourceGroup

# Setup keyvault policies to allow the app read the secrets
principalId="<PRINCIPAL ID FROM PREVIOUS STEP>"
az keyvault set-policy --name "$keyVaultName" --object-id "$principalId" --secret-permissions get list
```
