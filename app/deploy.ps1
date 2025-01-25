param (
    [string]$version
)

if (-not $version) {
    Write-Host "Please provide a version number."
    exit 1
}

## build the image
docker build . -t crrentcrlfc.azurecr.io/app:$version

Write-Host "Build image completed."

## sign in to the container registry
az acr login --name crrentcrlfc

Write-Host "Acr login completed."

## push the image to Azure
docker push crrentcrlfc.azurecr.io/app:$version

Write-Host "Docker push completed."

## update azure container app
az containerapp update `
    --name ca-rentcrl-stg-fc `
    --resource-group rg-rentcrl-stg-fc `
    --image crrentcrlfc.azurecr.io/app:$version `
    --cpu 0.5 `
    --memory 1.0Gi `
    --min-replicas 1 `
    --max-replicas 2
    

Write-Host "Containerapp update completed."