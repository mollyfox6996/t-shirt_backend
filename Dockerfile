FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app

COPY ["API/API.csproj", "API/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["aspnetapp.pfx", "/https/aspnetapp.pfx"]
COPY *.sln ./

RUN update-ca-certificates
RUN dotnet restore 

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS build
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=https://0.0.0.0:5000
ENV ASPNETCORE_HTTPS_PORT=5000
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=Password_0001
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx


COPY --from=base /app/out .
ENTRYPOINT ["dotnet", "API.dll"]
