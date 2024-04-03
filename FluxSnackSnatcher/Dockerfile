FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . ./

RUN dotnet restore

RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p ./www
COPY ./www/script.js ./www/script.js

EXPOSE 5000
ENTRYPOINT ["dotnet", "FluxSnackSnatcher.dll"]

