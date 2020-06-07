FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /repo

COPY /OcelotAPIGateway.csproj ./OcelotAPIGateway.csproj

RUN dotnet clean 
RUN dotnet clean

COPY . ./

RUN dotnet publish ./OcelotAPIGateway.csproj -c Release -o /app/

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

COPY --from=build /app/ /app/
WORKDIR /app

ENTRYPOINT ["dotnet", "OcelotAPIGateway.dll"]