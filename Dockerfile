#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["helloworlddocker.csproj", ""]
RUN dotnet restore "./helloworlddocker.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "helloworlddocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "helloworlddocker.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "helloworlddocker.dll"]
