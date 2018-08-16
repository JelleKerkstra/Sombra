FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Sombra.sln ./
COPY Sombra.Web.UnitTests/Sombra.Web.UnitTests.csproj Sombra.Web.UnitTests/

COPY Sombra.Web.UnitTests/ Sombra.Web.UnitTests/
WORKDIR /src/Sombra.Web.UnitTests
RUN dotnet build -c Debug -o /app

RUN dotnet test
