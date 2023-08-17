<!-- [![Actions Status Web](https://github.com/tremorscript/aspnetcoreangular/workflows/Web/badge.svg)](https://github.com/tremorscript/aspnetcoreangular/actions?query=workflow%3AWEB)
[![Actions Status STS](https://github.com/tremorscript/aspnetcoreangular/workflows/STS/badge.svg)](https://github.com/tremorscript/aspnetcoreangular/actions?query=workflow%3ASTS)
[![Build Status](https://tremorscript.visualstudio.com/playground/_apis/build/status/tremorscript.AspNetCoreAngular?branchName=master)](https://tremorscript.visualstudio.com/playground/_build/latest?definitionId=20&branchName=master)
[![Build status](https://ci.appveyor.com/api/projects/status/35j3sxdi22rhg70c?svg=true)](https://ci.appveyor.com/project/asadsahi/aspnetcoreangular)
[![MIT license](http://img.shields.io/badge/license-MIT-brightgreen.svg)](http://opensource.org/licenses/MIT)
 -->

[![Actions Status Web](https://github.com/tremorscript/aspnetcoreangular/workflows/Web/badge.svg)](https://github.com/tremorscript/aspnetcoreangular/actions?query=workflow%3AWEB)
[![Actions Status STS](https://github.com/tremorscript/aspnetcoreangular/workflows/STS/badge.svg)](https://github.com/tremorscript/aspnetcoreangular/actions?query=workflow%3ASTS)
[![License: Unlicense](https://img.shields.io/badge/license-Unlicense-blue.svg)](http://unlicense.org/)

## Features

- [ASP.NET Core 7.0](http://www.dot.net/)
- [Entity Framework Core 7.0](https://docs.efproject.net/en/latest/)
  - Both Sql Server and Sql lite databases are supported (Check installation instrcutions for more details)
- [Identity Server 4](http://identityserver.io/)
- [Angular 16](https://angular.io/)
- [Angular CLI 16](https://cli.angular.io/)
- Secure - with [Custom Security Policy (CSP) and custom security headers](https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders)
- [SASS](http://sass-lang.com/)
- Best [practices](https://angular.io/docs/ts/latest/guide/style-guide.html) for Angular code organisation.
- [Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture) inspired from Jason Taylor.
- Angular application inspired from [asadsahi](https://github.com/asadsahi/AspNetCoreSpa) and [gothinkster](https://github.com/gothinkster/realworld).
- [PWA support](https://developers.google.com/web/progressive-web-apps/)
- Unit Testing with [Jest](https://facebook.github.io/jest/).
- E2E testing with [Playwright](https://playwright.dev/).
- [Compodoc](https://compodoc.github.io/compodoc/) for Angular documentation
- Angular dynamic forms for reusable and DRY code.
- [Swagger](http://swagger.io/) as Api explorer.

## Developer Documentation

https://tremorscript.github.io/AspNetCoreAngular/ (In Progress)

## Pull Request Guidelines

https://tremorscript.github.io/AspNetCoreAngular/homesite/current/pull-request-guidelines.html

## Code Quality Guidelines

https://tremorscript.github.io/AspNetCoreAngular/homesite/current/code-quality-guidelines.html


## Getting Started 

1. Clone the repo:

   git clone https://github.com/tremorscript/AspNetCoreAngular

2. Change directory:

   cd AspNetCoreAngular

3. Update the local git config with:
   
   - [a list of alias commands](https://tremorscript.github.io/AspNetCoreAngular/homesite/current/pull-request-guidelines.html#_add_aliases_to_the_config_file_in_git).

4. Restore packages:

   dotnet restore AspNetCoreAngular.sln

5. Install npm packages:

   - cd src/Presentation/Web/ClientApp:

   - npm install

6. Start Frontend:

   - npm start

7. Run Backend:

   - Using [VSCode](https://code.visualstudio.com/):

     - If you are running for the first time, install dev certificates using command:

     ```
     dotnet dev-certs https --trust
     ```

     - From debug menu select `Web` profile to run api application
     - From debug menu select `STS` profile to run Identity Server application
     - From debug menu select `Firefox\Web\STS` profile to run the angular application, the api application and the Identity Server application.

   - Using [Visual Studio IDE](https://www.visualstudio.com/):
     - Run `Web` and `STS` projects either individually or by setting multiple projects in solutions properties and hit F5

8. Target either Sqlite or Microsoft SQL Server

This project supports both databases OOTB.

- Run with Sqlite: (Already configured to quickly run the project)

  - Project is already setup with Sqlite specific database migrations

- Run with Microsoft SQL Server:
  - Delete `Migrations` folder from src/Infrastructure/Infrastructure/Persistence
  - Change setting in appsettings.json called `useSqLite` from `true` to `false` and change `Web` connection string to your local Sql Server connection string

9. Once the project is running use following test users to login:

  - Test user1:  
    Username: admin@admin.com
    Password: P@ssw0rd!
  - Testu user2:  
   Username: user@user.com
   Password: P@ssw0rd!

   Note: For production use Identity server hosted with appropriate configuration.

# Managing Migrations

## Make sure you have ef core global tools installed

`dotnet tool install --global dotnet-ef`

# Web Migrations

## Using command line (from root of the project)

### Create Migration

`dotnet ef migrations add migrationname --startup-project ./src/Presentation/Web --project ./src/Infrastructure/Infrastructure --context ApplicationDbContext -o Persistence/Migrations`

### Update database

`dotnet ef database update --startup-project ./src/Presentation/Web --project ./src/Infrastructure/Infrastructure --context ApplicationDbContext`

### Drop database

`dotnet ef database drop --startup-project ./src/Presentation/Web --project ./src/Infrastructure/Infrastructure --context ApplicationDbContext`

# Localization Migrations

## Using command line (from root of the project)

### Create Migration

`dotnet ef migrations add migrationname --startup-project ./src/Presentation/Web --project ./src/Infrastructure/Infrastructure --context LocalizationDbContext -o Localization/Migrations`

### Update database

`dotnet ef database update --startup-project ./src/Presentation/Web --project ./src/Infrastructure/Infrastructure --context LocalizationDbContext`

### Drop database

`dotnet ef database drop --startup-project ./src/Presentation/Web --project ./src/Infrastructure/Infrastructure --context LocalizationDbContext`

# Identity Migrations

## Using command line (from root of the project)

### Create Migration

`dotnet ef migrations add migrationname --startup-project ./src/Presentation/STS --project ./src/Infrastructure/Infrastructure --context IdentityServerDbContext -o Identity/Migrations`

### Update database

`dotnet ef database update --startup-project ./src/Presentation/STS --project ./src/Infrastructure/Infrastructure --context IdentityServerDbContext`

### Drop database

`dotnet ef database drop --startup-project ./src/Presentation/STS --project ./src/Infrastructure/Infrastructure --context IdentityServerDbContext`

# Other commands

### Angular component scaffolding

Note: You need to run commands from `src/Presentation/Web/ClientApp` directory: More information [here](https://angular.io/cli)

### Angular tests - Using [Jest](https://jestjs.io/en/) and Angular jest [preset](https://github.com/thymikee/jest-preset-angular)

```bash
cd src/Presentation/Web/ClientApp

npm test
```

### Compodoc Angular documentation

- Steps to generate:
  - npm i compodoc -g
  - cd src/Presentation/Web/ClientApp
  - npm run compodoc
  - cd documentation
  - http-server

Compodoc documentation: ![alt text](compodoc.jpg 'compodoc documentation')

````
### run end-to-end tests
```bash
# make sure you have your server running in another terminal (i.e run "dotnet run" command)
npm run e2e
````

### run Protractor's elementExplorer (for end-to-end)

```bash
npm run webdriver:start
# in another terminal
npm run e2e:live
```

# Azure Deploy

- You can set an environment variable for azure app deployment password
  Set-Item -path env:AzureAppPass -value passwordhere

```
From powershell:
./deploy-azure.ps1
```

# Deploy to heroku using its container service

### Replace your app name where it is `aspnetcoreangular`

- dotnet publish -c release
- docker build -t aspnetcoreangular ./bin/release/net7.0/publish
- heroku login
- heroku container:login
- docker tag aspnetcoreangular registry.heroku.com/aspnetcoreangular/web
- docker push registry.heroku.com/aspnetcoreangular/web
  Note: There is a `deploy.heroku.ps1` script included with this project which automates above steps.

# Deploy to Azure as App Service

Set-Item -path env:AzureAppPass -value passwordhere

```
From powershell:
./deploy-azure.ps1
```

# Developer Documentation website

[https://tremorscript.github.io/AspNetCoreAngular](https://tremorscript.github.io/AspNetCoreAngular)
