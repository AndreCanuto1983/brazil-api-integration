FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5030

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["brazil-api-integration/Brazil.Api.Integration.csproj", "Brazil.Api.Integration/"]
RUN dotnet restore "brazil-api-integration/Brazil.Api.Integration.csproj"
COPY . .
WORKDIR "/src/Brazil.Api.Integration"
RUN dotnet build "Brazil.Api.Integration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Brazil.Api.Integration.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Brazil.Api.Integration.dll"]