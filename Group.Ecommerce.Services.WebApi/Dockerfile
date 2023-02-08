#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /src
COPY . . 
WORKDIR /src/Group.Ecommerce.Services.WebApi
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /src/Group.Ecommerce.Services.WebApi/out ./

ENTRYPOINT ["dotnet", "Group.Ecommerce.Services.WebApi.dll"]