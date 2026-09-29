FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["HalachevAccounting.Domain/HalachevAccounting.Domain.csproj", "HalachevAccounting.Domain/"]
COPY ["HalachevAccounting.Infrastructure/HalachevAccounting.Infrastructure.csproj", "HalachevAccounting.Infrastructure/"]
COPY ["HalachevAccounting/HalachevAccounting.Api.csproj", "HalachevAccounting/"]
RUN dotnet restore "HalachevAccounting/HalachevAccounting.Api.csproj"

COPY . .
WORKDIR "/src/HalachevAccounting"
RUN dotnet publish "HalachevAccounting.Api.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble AS final
WORKDIR /app

RUN apt-get update     && apt-get install -y --no-install-recommends libgssapi-krb5-2 curl ca-certificates     && install -d /usr/share/postgresql-common/pgdg     && curl -fsSL https://www.postgresql.org/media/keys/ACCC4CF8.asc -o /usr/share/postgresql-common/pgdg/apt.postgresql.org.asc     && echo "deb [signed-by=/usr/share/postgresql-common/pgdg/apt.postgresql.org.asc] https://apt.postgresql.org/pub/repos/apt noble-pgdg main" > /etc/apt/sources.list.d/pgdg.list     && apt-get update     && apt-get install -y --no-install-recommends postgresql-client-18     && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "HalachevAccounting.Api.dll"]
