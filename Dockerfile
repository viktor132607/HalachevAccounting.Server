FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src
COPY ["HalachevAccounting/HalachevAccounting.Api.csproj", "HalachevAccounting/"]
RUN dotnet restore "HalachevAccounting/HalachevAccounting.Api.csproj"
COPY . .
WORKDIR "/src/HalachevAccounting"
RUN dotnet publish "HalachevAccounting.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HalachevAccounting.Api.dll"]