FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY CollectionManager.Api/CollectionManager.Api.csproj CollectionManager.Api/
RUN dotnet restore CollectionManager.Api/CollectionManager.Api.csproj

COPY CollectionManager.Api/ CollectionManager.Api/
RUN dotnet publish CollectionManager.Api/CollectionManager.Api.csproj \
    -c Release -o /app/publish --no-restore

RUN dotnet tool install --tool-path /tools dotnet-ef --version 10.0.9

RUN ASPNETCORE_ENVIRONMENT=Production /tools/dotnet-ef migrations bundle \
    --project CollectionManager.Api/CollectionManager.Api.csproj \
    --configuration Release \
    --target-runtime linux-x64 \
    --output /app/efbundle

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .
COPY --from=build --chmod=755 /app/efbundle ./efbundle

ENV ASPNETCORE_HTTP_PORTS=8080
USER app
EXPOSE 8080

ENTRYPOINT ["dotnet", "CollectionManager.Api.dll"]