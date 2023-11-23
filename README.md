# CCF DotnetCore WebAPI Template

CCF Clean Web API nuget template is ready-to-use project template for creating dotnet core minimal api using Clean Architecture, leveraging ccf clean web api features.

# Key features

- [x] Clean Architecture
    - [x] CQRS
    - [x] MediatR
    - [x] Repositories (Generic Repositories)
    - [x] Model Mapping (Automapper)
    - [x] Validation (FluentValidation)
    - [x] Caching
- [x] Dotnet Core Minimal API
- [x] Entity Framework Core (Database)
- [x] Swagger
- [x] API Versioning
- [x] Exception Handling
- [x] Logging (NLog)
- [x] Dependency Injection
- [x] Generic API Http Client Handler
- [x] Authentication Support

# Supported Versions

Built on .NET 6.0

# Getting started

1. Install CCF Clean Web API Template

    ```
    dotnet new install CCF.Clean.Dotnet.WebAPI 
    ```
    > NOTE: The template only needs to be installed once. Running this command again will update your version of the template. Specify the version to get specific version of template.

2. Create a new directory

    ```    
    mkdir CCFCleanWeb 
    cd CCFCleanWeb
    ```

3. Create a new solution

    ```
    dotnet new CCFClean.WebApi --name {{SolutionName}} --output .\
    ```
    > NOTE: Specify {{SolutionName}}, this will be used as the solution name and project namespaces.


