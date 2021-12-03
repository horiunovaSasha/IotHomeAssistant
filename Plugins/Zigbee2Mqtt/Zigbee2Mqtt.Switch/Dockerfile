#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Zigbee2Mqtt.Switch/Zigbee2Mqtt.Switch.csproj", "Zigbee2Mqtt.Switch/"]
RUN dotnet restore "Zigbee2Mqtt.Switch/Zigbee2Mqtt.Switch.csproj"
COPY . .
WORKDIR "/src/Zigbee2Mqtt.Switch"
RUN dotnet build "Zigbee2Mqtt.Switch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zigbee2Mqtt.Switch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zigbee2Mqtt.Switch.dll"]