FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["api-integracao-ibge/api-integracao-ibge.csproj", "api-integracao-ibge/"]
RUN dotnet restore "api-integracao-ibge/api-integracao-ibge.csproj"
COPY . .
WORKDIR "/src/api-integracao-ibge"
RUN dotnet build "api-integracao-ibge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api-integracao-ibge.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api-integracao-ibge.dll"]