# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5173
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["web_rest_hudz_kp21.csproj", "web_rest_hudz_kp21/"]
RUN dotnet restore "./web_rest_hudz_kp21/web_rest_hudz_kp21.csproj"

WORKDIR "/src/web_rest_hudz_kp21"
COPY . .
RUN dotnet build "./web_rest_hudz_kp21.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./web_rest_hudz_kp21.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER "0:0"
ENTRYPOINT ["dotnet", "web_rest_hudz_kp21.dll"]