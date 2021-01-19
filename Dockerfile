FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
EXPOSE 5000

COPY * ./ 

ENTRYPOINT ["./API"]
