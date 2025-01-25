# RentCRL

RentCRL is a small web application that allows you to automate your rent payments.

# How To Deploy The Solution To Azure

To deploy to Azure, navigate to the folder `./app` and run the following commands. Do not forget to replace `{version}` with the actual application version. For instance `02`.

```
az login

.\deploy.ps1 -Version {version}

```
