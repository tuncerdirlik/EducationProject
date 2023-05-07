FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
COPY src/Gateways/Gateway/*.csproj src/Gateways/Gateway/

RUN dotnet restore src/Gateways/Gateway/*.csproj
COPY . .
RUN dotnet publish src/Gateways/Gateway/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Gateway.dll"]