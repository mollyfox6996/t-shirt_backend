FROM mcr.microsoft.com/dotnet/sdk:3.1 AS runtime
WORKDIR /app
EXPOSE 5000

COPY *.dll /app/out/ 

ENTRYPOINT ["dotnet", "API.dll"]
