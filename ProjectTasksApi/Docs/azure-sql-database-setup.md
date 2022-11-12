```bash
az account list # Will list my subscription with name "Azure subscription 1"

az account set --subscription "Azure subscription 1" # Not required if thereis only one subscription available

let "randomIdentifier=$RANDOM*$RANDOM"
location="westeurope"
resourceGroup="project_tasks-azuresql-rg-$randomIdentifier"
tag="create-and-configure-database"
server="project-tasks-azuresql-server-$randomIdentifier"
database="project_tasks_azuresql_db_$randomIdentifier"
login="project_tasks_root_user"
password="**************"
startIp=87.116.190.42 # My public IP
endIp=87.116.190.42 # My public IP

# Create a resource group
az group create --name $resourceGroup --location "$location" --tags $tag

az sql server create --name $server --resource-group $resourceGroup --location "$location" --admin-user $login --admin-password $password

az sql server firewall-rule create --resource-group $resourceGroup --server $server -n AllowYourIp --start-ip-address $startIp --end-ip-address $endIp

az sql db create \
    --resource-group $resourceGroup \
    --server $server \
    --name $database \
    --sample-name AdventureWorksLT \
    --edition GeneralPurpose \
    --compute-model Serverless \
    --family Gen5 \
    --capacity 2
```

Then save the connection string to secrets:

```bash
dotnet user-secrets set "AzureSqlConnection" "<MY CONNECTION STRING>"
```
