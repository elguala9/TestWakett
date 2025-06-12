
# Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY RateController/RateController.csproj RateController/
COPY Rates.Core/Rates.Core.csproj Rates.Core/
COPY Rates.Infrastracture/Rates.Infrastracture.csproj Rates.Infrastracture/

# Restore
RUN dotnet restore RateController/RateController.csproj

# Copy source
COPY RateController/ RateController/
COPY Rates.Core/ Rates.Core/
COPY Rates.Infrastracture/ Rates.Infrastracture/

# Build
WORKDIR /src/RateController
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish RateController.csproj -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RateController.dll"]
