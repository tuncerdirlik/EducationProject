FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80

COPY src/Core/Core.CacheService/*.csproj src/Core/Core.CacheService/
COPY src/MicroServices/EducationGroupMicroService/EducationGroupMicroService.Application/*.csproj src/MicroServices/EducationGroupMicroService/EducationGroupMicroService.Application/
COPY src/MicroServices/EducationGroupMicroService/EducationGroupMicroService.Domain/*.csproj src/MicroServices/EducationGroupMicroService/EducationGroupMicroService.Domain/
COPY src/MicroServices/EducationGroupMicroService/EducationGroupMicroService.Persistence/*.csproj src/MicroServices/EducationGroupMicroService/EducationGroupMicroService.Persistence/
COPY src/MicroServices/EducationGroupMicroService/EducationGroupMicroService.Api/*.csproj src/MicroServices/EducationGroupMicroService/EducationGroupMicroService.Api/

RUN dotnet restore src/MicroServices/EducationGroupMicroService/EducationGroupMicroService.Api/*.csproj
COPY . .
RUN dotnet publish src/MicroServices/EducationGroupMicroService/EducationGroupMicroService.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "EducationGroupMicroService.Api.dll"]