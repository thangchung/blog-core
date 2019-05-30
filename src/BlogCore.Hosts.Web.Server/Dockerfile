FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/BlogCore.Hosts.Web.Server/BlogCore.Hosts.Web.Server.csproj", "src/BlogCore.Hosts.Web.Server/"]
COPY ["src/BlogCore.Hosts.Web.Client/BlogCore.Hosts.Web.Client.csproj", "src/BlogCore.Hosts.Web.Client/"]
COPY ["src/BlogCore.Modules.BlogContext/BlogCore.Modules.BlogContext.csproj", "src/BlogCore.Modules.BlogContext/"]
COPY ["src/BlogCore.Shared/BlogCore.Shared.csproj", "src/BlogCore.Shared/"]
COPY ["src/BlogCore.Modules.CommonContext/BlogCore.Modules.CommonContext.csproj", "src/BlogCore.Modules.CommonContext/"]
RUN dotnet restore "src/BlogCore.Hosts.Web.Server/BlogCore.Hosts.Web.Server.csproj"
COPY . .
WORKDIR "/src/src/BlogCore.Hosts.Web.Server"
RUN dotnet build "BlogCore.Hosts.Web.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BlogCore.Hosts.Web.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BlogCore.Hosts.Web.Server.dll"]