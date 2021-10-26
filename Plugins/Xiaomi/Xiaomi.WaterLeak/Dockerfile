#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Xiaomi.WaterLeak/Xiaomi.WaterLeak.csproj", "Xiaomi.WaterLeak/"]
RUN dotnet restore "Xiaomi.WaterLeak/Xiaomi.WaterLeak.csproj"
COPY . .
WORKDIR "/src/Xiaomi.WaterLeak"
RUN dotnet build "Xiaomi.WaterLeak.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Xiaomi.WaterLeak.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Xiaomi.WaterLeak.dll"]