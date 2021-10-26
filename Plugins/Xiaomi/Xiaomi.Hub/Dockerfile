#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Xiaomi.Hub/Xiaomi.Hub.csproj", "Xiaomi.Hub/"]
RUN dotnet restore "Xiaomi.Hub/Xiaomi.Hub.csproj"
COPY . .
WORKDIR "/src/Xiaomi.Hub"
RUN dotnet build "Xiaomi.Hub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Xiaomi.Hub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Xiaomi.Hub.dll"]