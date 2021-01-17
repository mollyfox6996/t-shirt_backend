FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS build
WORKDIR /app
EXPOSE 5000

COPY *.dll /app/out 

ENTRYPOINT ["dotnet", "API.dll"]
