#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Zigbee2Mqtt.RemoteSingleButton/Zigbee2Mqtt.RemoteSingleButton.csproj", "Zigbee2Mqtt.RemoteSingleButton/"]
RUN dotnet restore "Zigbee2Mqtt.RemoteSingleButton/Zigbee2Mqtt.RemoteSingleButton.csproj"
COPY . .
WORKDIR "/src/Zigbee2Mqtt.RemoteSingleButton"
RUN dotnet build "Zigbee2Mqtt.RemoteSingleButton.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zigbee2Mqtt.RemoteSingleButton.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zigbee2Mqtt.RemoteSingleButton.dll"]