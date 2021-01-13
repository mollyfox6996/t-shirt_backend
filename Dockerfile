FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app

COPY ["API/API.csproj", "API/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Services/Services.csproj", "Services/"]
COPY *.sln ./

RUN update-ca-certificates
RUN dotnet restore 

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS build
WORKDIR /app

COPY --from=base /app/out .

FROM nginx:1.17.1-alpine
COPY nginx.conf /etc/nginx/
EXPOSE 5000
ENTRYPOINT ["dotnet", "API.dll"]
