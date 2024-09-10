#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/AviationFleetService/AviationFleetService.Api/AviationFleetService.Api.csproj", "src/Services/AviationFleetService/AviationFleetService.Api/"]
COPY ["src/Services/AviationFleetService/AviationFleetService.Infrastructure/AviationFleetService.Infrastructure.csproj", "src/Services/AviationFleetService/AviationFleetService.Infrastructure/"]
COPY ["src/Services/AviationFleetService/AviationFleetService.Application/AviationFleetService.Application.csproj", "src/Services/AviationFleetService/AviationFleetService.Application/"]
COPY ["src/Services/AviationFleetService/AviationFleetService.Domain/AviationFleetService.Domain.csproj", "src/Services/AviationFleetService/AviationFleetService.Domain/"]
RUN dotnet restore "./src/Services/AviationFleetService/AviationFleetService.Api/AviationFleetService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/AviationFleetService/AviationFleetService.Api"
RUN dotnet build "./AviationFleetService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AviationFleetService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AviationFleetService.Api.dll"]