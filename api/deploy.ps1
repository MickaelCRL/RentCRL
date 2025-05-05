param (
    [string]$version
)

if (-not $version) {
    Write-Host "Please provide a version number."
    exit 1
}

$registryName = "crrentcrlfc"
$imageName = "$registryName.azurecr.io/api:$version"

## build the solution
# dotnet restore RentCRL.Web/RentCRL.Web.csproj
# dotnet publish RentCRL.Web/RentCRL.Web.csproj -c Release -o ./publish

## build the image
docker build -t $imageName -f RentCRL.Web/Dockerfile .

Write-Host "Build image completed."

## sign in to the container registry
az acr login --name $registryName

Write-Host "Acr login completed."

## push the image to Azure
docker push $imageName

Write-Host "Docker push completed."

## update azure container app
az containerapp update `
    --name ca-rentcrl-api-stg-fc `
    --resource-group rg-rentcrl-stg-fc `
    --image $imageName `
    --cpu 0.5 `
    --memory 1.0Gi `
    --min-replicas 1 `
    --max-replicas 2 `
    --set-env-vars `
    ASPNETCORE_ENVIRONMENT=Staging `
    CosmosDB__PrimaryKey=secretref:cosmosdb-primary-key
    
Write-Host "Containerapp update completed."