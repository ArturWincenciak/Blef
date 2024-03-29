[![v2.0 CleanUp Code](https://github.com/ArturWincenciak/Blef/actions/workflows/ci-cleanup-code.yml/badge.svg)](https://github.com/ArturWincenciak/Blef/actions/workflows/ci-cleanup-code.yml)
[![v2.0 Inspect Code](https://github.com/ArturWincenciak/Blef/actions/workflows/ci-inspect-code.yml/badge.svg)](https://github.com/ArturWincenciak/Blef/actions/workflows/ci-inspect-code.yml)
[![v2.1 SonarCloud Analyze](https://github.com/ArturWincenciak/Blef/actions/workflows/ci-sonar-cloud-analyzy.yml/badge.svg)](https://github.com/ArturWincenciak/Blef/actions/workflows/ci-sonar-cloud-analyzy.yml)
[![v2.0 Unit Tests](https://github.com/ArturWincenciak/Blef/actions/workflows/ci-unit-tests.yml/badge.svg)](https://github.com/ArturWincenciak/Blef/actions/workflows/ci-unit-tests.yml)
[![CodeQL](https://github.com/ArturWincenciak/Blef/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/ArturWincenciak/Blef/actions/workflows/codeql-analysis.yml)
[![CodeFactor](https://www.codefactor.io/repository/github/arturwincenciak/blef/badge)](https://www.codefactor.io/repository/github/arturwincenciak/blef)

[![SonarCloud](https://sonarcloud.io/images/project_badges/sonarcloud-black.svg)](https://sonarcloud.io/summary/new_code?id=ArturWincenciak_Blef)

# Blef

## Card game

- Docker Hub image: https://hub.docker.com/repository/docker/teovincent/blef
- Specification: https://blef.azurewebsites.net/swagger/index.html

## How to run for the first time

```cmd
$ dotnet run --project ./src/Blef.Bootstrapper/Blef.Bootstrapper.csproj
```
_then open your web browser to https://localhost:49153/swagger_

> **_NOTE:_** Web Application will run HTTPS without certificate. To fix it run `dotnet dev-certs https --trust` as described in https://www.hanselman.com/blog/developing-locally-with-aspnet-core-under-https-ssl-and-selfsigned-certs

#### Using `Dockerfile`
```cmd
$ docker build -t blef-dev .
$ docker run -dp 3000:80 blef-dev
```
_then open your web browser to http://localhost:3000/swagger_

#### Using Docker Hub
```cmd
$ docker run -dp 5000:80 teovincent/blef:latest
```
_then open your web browser to http://localhost:5000/swagger_

## Project diagram

The starting point is the [`Blef.Bootstrapper`](./src/Blef.Bootstrapper/Program.cs) project that is responsible for loading modules and launching the service.

![project diagram](https://github.com/ArturWincenciak/Blef/assets/9107578/3377b31e-aac7-4166-afd8-91d309f77690)

Starting points of modules are in API projects, specifically in `Blef.Modules.*.Api` projects. Every API project contains a class that implements the [`IModule`](./src/Shared/Blef.Shared.Abstractions/Modules/IModule.cs) interface.

Currently, the project consists of two modules: `Games` and `Users`. The `Games` module has a starting point defined in the [`GamesModule`](./src/Modules/Games/Blef.Modules.Games.Api/GamesModule.cs) class, and the `Users` module has a starting point defined in the [`UsersModule`](./src/Modules/Users/Blef.Modules.Users.Api/UsersModule.cs) class.

Each API project module has controller definitions and its own JSON configuration files.



