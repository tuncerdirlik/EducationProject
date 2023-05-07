FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80

COPY src/Core/Core.CacheService/*.csproj src/Core/Core.CacheService/
COPY src/MicroServices/EducationMicroService/EducationMicroService.Application/*.csproj src/MicroServices/EducationMicroService/EducationMicroService.Application/
COPY src/MicroServices/EducationMicroService/EducationMicroService.Domain/*.csproj src/MicroServices/EducationMicroService/EducationMicroService.Domain/
COPY src/MicroServices/EducationMicroService/EducationMicroService.Persistance/*.csproj src/MicroServices/EducationMicroService/EducationMicroService.Persistance/
COPY src/MicroServices/EducationMicroService/EducationMicroService.Api/*.csproj src/MicroServices/EducationMicroService/EducationMicroService.Api/

RUN dotnet restore src/MicroServices/EducationMicroService/EducationMicroService.Api/*.csproj
COPY . .
RUN dotnet publish src/MicroServices/EducationMicroService/EducationMicroService.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "EducationMicroService.Api.dll"]