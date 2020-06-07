FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /repo

COPY /UserService.csproj ./UserService.csproj

RUN dotnet restore
RUN dotnet clean

COPY . ./

RUN dotnet publish ./UserService.csproj -c Release -o /app/

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

COPY --from=build /app/ /app/
WORKDIR /app

ENTRYPOINT ["dotnet", "UserService.dll"]