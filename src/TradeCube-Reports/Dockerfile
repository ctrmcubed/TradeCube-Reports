FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
# COPY ["NuGet.Config", "TradeCube-Reports/"]
COPY ["TradeCube-Reports/", "TradeCube-Reports/"]

RUN dotnet restore "TradeCube-Reports/TradeCube-Reports.csproj"

WORKDIR "/src/TradeCube-Reports"
RUN dotnet build "TradeCube-Reports.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TradeCube-Reports.csproj" -c Release -o /app

COPY ["TradeCube-Reports/appsettings.*", "/app/"]

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TradeCube-Reports.dll"]