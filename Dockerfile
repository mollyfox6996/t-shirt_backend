FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
EXPOSE 5000

COPY . ./ 

ENTRYPOINT ["dotnet", "API.dll"]
