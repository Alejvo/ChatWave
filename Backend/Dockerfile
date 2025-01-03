FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY *.sln ./
COPY Shared/ ./Shared/
COPY Domain/ ./Domain/
COPY Infrastructure/ ./Infrastructure/
COPY Application/ ./Application/
COPY WebApi/ ./WebApi/

RUN dotnet restore ./WebApi/WebApi.csproj

RUN dotnet publish ./WebApi/WebApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "WebApi.dll"]
