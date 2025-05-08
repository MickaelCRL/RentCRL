param (
    [string]$version
)

if (-not $version) {
    Write-Host "Please provide a version number."
    exit 1
}

$containerAppName = "ca-rentcrl-app-stg-fc"
$resourceGroupName = "rg-rentcrl-stg-fc"
$registryName = "crrentcrlfc"
$registryServerName = "$registryName.azurecr.io"
$imageName = "$registryServerName/app:$version"

## build react solution
npm run build

## build the image
docker build . -t $imageName

Write-Host "Build image completed."

## sign in to the container registry
az acr login --name $registryName

Write-Host "Acr login completed."

## push the image to Azure
docker push $imageName

Write-Host "Docker push completed."

az containerapp registry set `
    -n $containerAppName  `
    -g $resourceGroupName `
    --server $registryServerName `
    --identity system

## update azure container app
az containerapp update `
    --name $containerAppName `
    --resource-group $resourceGroupName `
    --image $imageName `
    --cpu 0.5 `
    --memory 1.0Gi `
    --min-replicas 1 `
    --max-replicas 2 `
    --set-env-vars `
    API_BASE_URL=https://ca-rentcrl-api-stg-fc.politewater-4ee63bcd.francecentral.azurecontainerapps.io
    
Write-Host "Containerapp update completed."