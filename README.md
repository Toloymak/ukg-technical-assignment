# UKG - HCM
Test assignment for the company UKG

## Task Requirements
1.	PEOPLE MANAGEMENT:
      -	IMPLEMENT CRUD (CREATE, READ, UPDATE, DELETE) OPERATIONS FOR MANAGING PEOPLE RECORDS.
2.	USER AUTHENTICATION & AUTHORIZATION:
      -	IMPLEMENT A LOGIN MODULE
      - THE SYSTEM SHOULD HAVE AT LEAST COUPLE OF ROLES LIKE:
        - EMPLOYEE
        - MANAGER 
        - HR ADMIN 
      - RESTRICT FUNCTIONALITIES BASED ON ROLES.
3.	USER INTERFACE & NAVIGATION:
      - IT IS UP TO YOU TO DECIDE WHAT TO USE AND HOW TO STRUCTURE IT
4.	TESTING REQUIREMENTS
      - UNIT TESTS
      - INTEGRATION TESTS
      - UI TESTS (OPTIONAL)


## What's done
- The project is built using ASP.NET Core Web API and Blazor for the front-end.\
Below used solution structure for describing the project, not the folder structure - solution contains solution-folders
- **Web API** structure is based on Clean Architecture principles (with some simplification to save the dev time), which separates the application into different layers:
    - **WebApi**: The main entry point for the API. Used MinimalApis.
      - Implements CRUD operations for managing people records: Create, Read, Update, and Delete.
      - Custom login endpoint/module is implemented using JWT tokens
      - Used Authorization to restrict access to certain endpoints based on user (account) roles.
    - **Application**: Contains the business logic and application services. Also includes anemic **"Domain"** models.
    - **Infrastructure**: Contains the implementation of data access and external services.
    - **Tests**: Contains unit and integration tests.
      - **Unit tests**: Contains unit tests for all layers.
        - Endpoint cover only base cases for "People CRUD"
      - **Integration tests**: Contains integration tests for the Web API. Used WebApplicationFactory + Test container for MsSQL DB.
        - There is only few tests for people CRUD operations
    - There is enable DB migration and seeding of some people/roles on application starting
    - Supports Swagger
- **Web UI**
    - **Blazor**: The front-end of the application, built using Blazor components
    - Application support authentication and authorization using JWT tokens.
    - Supported Person CRUD operations.
    - Application doesn't use any patterns for the front-end, but it is possible to implement MVVM or similar patterns in the future.
    - It's a single-page application (SPA) and auth is not saving between page reloads.
- **Common**: Contains common code shared between the Web API and Blazor projects.
    - **DTOs**: Contains Data Transfer Objects (DTOs) used for communication between the API and the front-end.

## Technologies used
- Minimal APIs
- JWT authentication
- MsSQL DB
- FluentValidation
- EntityFrameworkCore
- Blazor
- LanguageExt to support operation results
- Aspire to run services (almost without the configuration)

## What can be improved
- **Web API**
    - Implement more complex domain models and use a separate layer for them
    - Implement more complex validation mechanisms
    - Rewrite seeding to avoid direct calls to the DB context
    - Use Discriminated Unions in operation results to make result more obvious
- **Authentication**
  - Instead of using custom JWT tokens, use IdentityServer/Keycloak for authentication
    - Custom solution required to many things to be implemented (e.g. refresh tokens, logout, etc.)
    - Implemented only basic authentication and authorization, and it's too expensive to implement all the features
    - Security in the supported in certificated solutions will be better
- **Web UI**
  - Implement more complex patterns for the front-end (e.g. MVVM, etc.)
  - Keep authentication between page reloads
  - Implement more complex validation mechanisms
- **Common**
  - Use Refit clients to keep the code clean and easy to maintain
- **E2E tests**
  - Implement E2E tests for the Web UI
- **Unit and Integration tests**
  - Add more tests to cover more logic


## How to run the project
1. Configure MsSQL DB ro local launching
```
docker run \
  -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=Password123" \
  -p 1433:1433 \
  --name UKG.HCM \
  -d mcr.microsoft.com/mssql/server:2022-latest
```
2. Run Azure Aspire service or WebApi and WebUI projects separately

## Default seeds
You can log in to the app using following credentials:
- **Admin** role: admin/Pass1234
- **Manager** role: manager/Pass1234
- **Employee** role: employee/Pass1234

# Run integration tests
1. Pull the latest MsSQL image
```
docker pull mcr.microsoft.com/mssql/server:2022-CU13-ubuntu-22.04
```
2. Run tests in WebApi.IntegrationTests