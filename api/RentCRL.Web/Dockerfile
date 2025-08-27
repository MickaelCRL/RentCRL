# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .
WORKDIR "/src/RentCRL.Web"
RUN dotnet restore "RentCRL.Web.csproj"
RUN dotnet build "RentCRL.Web.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "RentCRL.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage for running the application
FROM base AS final
ENV ASPNETCORE_HTTP_PORTS=80
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentCRL.Web.dll"]