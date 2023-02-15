FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
COPY . /src
WORKDIR /src/IdentityServer/IdentityServer
RUN dotnet publish -c Release -o /app 

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "IdentityServer.dll"]

