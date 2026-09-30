FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY CollectionManager.Web/CollectionManager.Web.csproj CollectionManager.Web/
RUN dotnet restore CollectionManager.Web/CollectionManager.Web.csproj

COPY CollectionManager.Web/ CollectionManager.Web/
RUN dotnet publish CollectionManager.Web/CollectionManager.Web.csproj \
    -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8080
RUN mkdir -p /home/app/.aspnet/DataProtection-Keys \
    && chown -R app:app /home/app/.aspnet \
    && chmod 700 /home/app/.aspnet/DataProtection-Keys
USER app
EXPOSE 8080

ENTRYPOINT ["dotnet", "CollectionManager.Web.dll"]