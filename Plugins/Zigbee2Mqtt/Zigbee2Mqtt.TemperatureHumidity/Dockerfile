#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Zigbee2Mqtt.Temperature/Zigbee2Mqtt.Temperature.csproj", "Zigbee2Mqtt.Temperature/"]
RUN dotnet restore "Zigbee2Mqtt.Temperature/Zigbee2Mqtt.Temperature.csproj"
COPY . .
WORKDIR "/src/Zigbee2Mqtt.Temperature"
RUN dotnet build "Zigbee2Mqtt.Temperature.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zigbee2Mqtt.Temperature.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zigbee2Mqtt.Temperature.dll"]