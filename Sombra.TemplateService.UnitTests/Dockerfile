FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.TemplateService.UnitTests/Sombra.TemplateService.UnitTests.csproj Sombra.TemplateService.UnitTests/
COPY . .
WORKDIR /src/Sombra.TemplateService.UnitTests

RUN dotnet build -c Debug -o /app

RUN dotnet test