FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app

COPY ["API/API.csproj", "API/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Services/Services.csproj", "Services/"]
COPY *.sln ./

RUN dotnet restore 

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS build
WORKDIR /app
EXPOSE 80

COPY --from=base /app/out .

ENTRYPOINT ["dotnet", "API.dll"]
