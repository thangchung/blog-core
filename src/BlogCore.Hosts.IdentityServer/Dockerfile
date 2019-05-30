FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/BlogCore.Hosts.IdentityServer/BlogCore.Hosts.IdentityServer.csproj", "src/BlogCore.Hosts.IdentityServer/"]
RUN dotnet restore "src/BlogCore.Hosts.IdentityServer/BlogCore.Hosts.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/BlogCore.Hosts.IdentityServer"
RUN dotnet build "BlogCore.Hosts.IdentityServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BlogCore.Hosts.IdentityServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BlogCore.Hosts.IdentityServer.dll"]