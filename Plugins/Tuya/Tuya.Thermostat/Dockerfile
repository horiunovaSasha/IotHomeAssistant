#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Tuya.Thermostat/Tuya.Thermostat.csproj", "Tuya.Thermostat/"]
COPY ["Tuya/Tuya.csproj", "Tuya/"]
RUN dotnet restore "Tuya.Thermostat/Tuya.Thermostat.csproj"
COPY . .
WORKDIR "/src/Tuya.Thermostat"
RUN dotnet build "Tuya.Thermostat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tuya.Thermostat.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tuya.Thermostat.dll"]