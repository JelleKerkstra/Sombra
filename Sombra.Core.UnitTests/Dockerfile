FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.Core.UnitTests/Sombra.Core.UnitTests.csproj Sombra.Core.UnitTests/
COPY . .
WORKDIR /src/Sombra.Core.UnitTests
RUN dotnet build -c Debug -o /app

RUN dotnet test