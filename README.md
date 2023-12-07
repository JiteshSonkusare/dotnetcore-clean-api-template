[![Build](https://github.com/JiteshSonkusare/dotnetcore-clean-api-template/actions/workflows/release.yml/badge.svg)](https://github.com/JiteshSonkusare/dotnetcore-clean-api-template/actions/workflows/release.yml) [![license](https://img.shields.io/github/license/JiteshSonkusare/dotnetcore-clean-api-template?color=blue&label=license&logo=Github&style=flat-square)](https://github.com/JiteshSonkusare/dotnetcore-clean-api-template/blob/master/README.md) [![nuget](https://img.shields.io/nuget/v/CCF.Clean.Dotnet.WebAPI?label=version&logo=NuGet&style=flat-square)](https://www.nuget.org/packages/CCF.Clean.Dotnet.WebAPI) [![nuget](https://img.shields.io/nuget/dt/CCF.Clean.Dotnet.WebAPI?color=blue&label=downloads&logo=NuGet&style=flat-square)](https://www.nuget.org/packages/CCF.Clean.Dotnet.WebAPI)

# CCF Clean WebAPI DotnetCore Template

CCF Clean Web API nuget template is ready-to-use project template for creating dotnet core minimal api using Clean Architecture, leveraging ccf clean web api features.

## Key features

- [x] Clean Architecture
    - [x] CQRS
    - [x] MediatR
    - [x] Repositories (Generic Repositories)
    - [x] Model Mapping (Automapper)
    - [x] Validation (FluentValidation)
    - [x] Memory Caching
- [x] Dotnet Core Minimal API
- [x] Entity Framework Core (Database)
- [x] Swagger
- [x] API Versioning
- [x] Global Exception Handling
- [x] Logging (NLog)
- [x] Dependency Injection
- [x] Generic API Http Client Handler
- [x] Authentication Support
- [x] Option Pattern

## Supported Versions

- [x] .NET 6.0
- [x] .NET 7.0
- [x] .NET 8.0

## Getting started

1. Install CCF Clean Web API Template

    ```
    dotnet new install CCF.Clean.Dotnet.WebAPI 
    ```
    > NOTE: The template only needs to be installed once. Running this command again will update your version of the template. Specify the version to get specific version of template.

2. Create a new directory

    ```    
    mkdir CCFDemoWebApp
    cd CCFDemoWebApp
    ```

3. Create a new solution

    ```
    dotnet new CCFClean.WebApi --name {{SolutionName}} --output .\
    ```
    > NOTE: Specify {{SolutionName}}, this will be used as the solution name and project namespaces.