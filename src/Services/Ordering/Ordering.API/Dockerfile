# Stage 1
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Stage 2
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Ordering/Ordering.API/Ordering.API.csproj", "Services/Ordering/Ordering.API/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
COPY ["src/Services/Ordering/Ordering.Application/Ordering.Application.csproj", "BuildingBlocks/Ordering/Ordering.Application/"]
COPY ["src/Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "BuildingBlocks/Ordering/Ordering.Infrastructure/"]
RUN dotnet restore "Services/Ordering/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "src/Services/Ordering/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish 

# Stage 4
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]
