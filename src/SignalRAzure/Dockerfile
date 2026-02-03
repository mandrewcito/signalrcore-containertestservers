# Imagen oficial de Azure Functions v4 para Python
FROM mcr.microsoft.com/azure-functions/python:4-python3.10

# Variables obligatorias
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

# Copiamos el código
COPY . /home/site/wwwroot

# Instalamos dependencias
RUN pip install --no-cache-dir -r /home/site/wwwroot/requirements.txt