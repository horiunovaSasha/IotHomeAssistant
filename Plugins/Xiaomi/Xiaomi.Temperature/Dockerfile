#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Xiaomi.Temperature/Xiaomi.Temperature.csproj", "Xiaomi.Temperature/"]
RUN dotnet restore "Xiaomi.Temperature/Xiaomi.Temperature.csproj"
COPY . .
WORKDIR "/src/Xiaomi.Temperature"
RUN dotnet build "Xiaomi.Temperature.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Xiaomi.Temperature.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Xiaomi.Temperature.dll"]