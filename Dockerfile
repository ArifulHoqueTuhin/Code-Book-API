
# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CodeBookAPL/CodeBookAPL.csproj", "CodeBookAPL/"]
COPY CodeBookAPL.sln ./
RUN dotnet restore "./CodeBookAPL/CodeBookAPL.csproj"

COPY . .
WORKDIR "/src/CodeBookAPL"
RUN dotnet build "./CodeBookAPL.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CodeBookAPL.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Required for Render
ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "CodeBookAPL.dll"]
