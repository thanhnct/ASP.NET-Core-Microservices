# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Basket.API/Basket.API.csproj", "Services/Basket.API/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
COPY ["src/BuildingBlocks/Contracts/Contracts.csproj", "BuildingBlocks/Contracts/"]
COPY ["src/BuildingBlocks/Infrastructure/Infrastructure.csproj", "BuildingBlocks/Infrastructure/"]
COPY ["src/BuildingBlocks/Shared/Shared.csproj", "BuildingBlocks/Shared/"]
RUN dotnet restore "Services/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "src/Services/Basket.API"
RUN dotnet build "Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish 

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]

