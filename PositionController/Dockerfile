
# Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY PositionController/PositionController.csproj PositionController/
COPY Positions.Core/Positions.Core.csproj Positions.Core/
COPY Positions.Infrastracture/Positions.Infrastracture.csproj Positions.Infrastracture/

# Restore
RUN dotnet restore PositionController/PositionController.csproj

# Copy source
COPY PositionController/ PositionController/
COPY Positions.Core/ Positions.Core/
COPY Positions.Infrastracture/ Positions.Infrastracture/

# Build
WORKDIR /src/PositionController
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish PositionController.csproj -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PositionController.dll"]
