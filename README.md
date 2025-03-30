# ukg-technical-assignment-
UKG BE ASSIGNMENT - HCM

# How to run the project
1. Configure MsSQL DB
```
docker run \
  -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=Password123" \
  -p 1433:1433 \
  --name UKG.HCM \
  -d mcr.microsoft.com/mssql/server:2022-latest
```


# Run integration tests
1. Pull the latest MsSQL image
```
docker pull mcr.microsoft.com/mssql/server:2022-CU13-ubuntu-22.04
```
2. Run tests in WebApi.IntegrationTests