FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /repo

COPY /ProfileService.csproj ./ProfileService.csproj

RUN dotnet restore
RUN dotnet clean

COPY . ./

RUN dotnet publish ./ProfileService.csproj -c Release -o /app/

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

COPY --from=build /app/ /app/
WORKDIR /app

ENTRYPOINT ["dotnet", "ProfileService.dll"]