#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/FlightService/FlightService.Api/FlightService.Api.csproj", "src/Services/FlightService/FlightService.Api/"]
COPY ["src/Services/FlightService/FlightService.Infrastructure/FlightService.Infrastructure.csproj", "src/Services/FlightService/FlightService.Infrastructure/"]
COPY ["src/Services/FlightService/FlightService.Application/FlightService.Application.csproj", "src/Services/FlightService/FlightService.Application/"]
COPY ["src/Services/FlightService/FlightService.Domain/FlightService.Domain.csproj", "src/Services/FlightService/FlightService.Domain/"]
RUN dotnet restore "./src/Services/FlightService/FlightService.Api/FlightService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/FlightService/FlightService.Api"
RUN dotnet build "./FlightService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FlightService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlightService.Api.dll"]