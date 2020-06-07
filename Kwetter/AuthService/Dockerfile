FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /repo

COPY /AuthService.csproj ./AuthService.csproj

RUN dotnet restore

COPY . ./

RUN dotnet publish ./AuthService.csproj -c Release -o /app/

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

COPY --from=build /app/ /app/
WORKDIR /app

ENTRYPOINT ["dotnet", "AuthService.dll"]