
# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
# RUN dotnet restore "./ClusterManagement.csproj" --disable-parallel
# RUN dotnet publish "./ClusterManagement.csproj" -c release -o /app --no-restore

# # Serve Stage
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app
# COPY --from=build /app ./

EXPOSE 5164

# ENTRYPOINT ["dotnet", "ClusterManagement.dll"]
