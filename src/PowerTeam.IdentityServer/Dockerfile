FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY PowerTeam.IdentityServer/PowerTeam.IdentityServer.csproj PowerTeam.IdentityServer/
RUN dotnet restore PowerTeam.IdentityServer/PowerTeam.IdentityServer.csproj
COPY . .
WORKDIR /src/PowerTeam.IdentityServer
RUN dotnet build PowerTeam.IdentityServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PowerTeam.IdentityServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PowerTeam.IdentityServer.dll"]
