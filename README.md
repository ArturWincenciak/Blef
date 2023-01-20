[![Clean Up and Inspect Code](https://github.com/ArturWincenciak/Blef/actions/workflows/ci-code-analyze.yml/badge.svg?branch=main)](https://github.com/ArturWincenciak/Blef/actions/workflows/ci-code-analyze.yml)
[![CodeQL](https://github.com/ArturWincenciak/Blef/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/ArturWincenciak/Blef/actions/workflows/codeql-analysis.yml)
[![CodeFactor](https://www.codefactor.io/repository/github/arturwincenciak/blef/badge)](https://www.codefactor.io/repository/github/arturwincenciak/blef)

[![SonarCloud](https://sonarcloud.io/images/project_badges/sonarcloud-black.svg)](https://sonarcloud.io/summary/new_code?id=ArturWincenciak_Blef)

# Blef

## Card game

- Docker Hub image: https://hub.docker.com/repository/docker/teovincent/blef
- Hosted App: https://blef.azurewebsites.net/
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
