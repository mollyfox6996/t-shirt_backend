FROM mcr.microsoft.com/dotnet/runtime:3.1 AS runtime
WORKDIR /app
EXPOSE 5000

COPY *.dll /app/out/ 

ENTRYPOINT ["dotnet", "API.dll"]
