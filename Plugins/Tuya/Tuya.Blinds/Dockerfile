#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Tuya.Blinds/Tuya.Blinds.csproj", "Tuya.Blinds/"]
COPY ["Tuya/Tuya.csproj", "Tuya/"]
RUN dotnet restore "Tuya.Blinds/Tuya.Blinds.csproj"
COPY . .
WORKDIR "/src/Tuya.Blinds"
RUN dotnet build "Tuya.Blinds.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tuya.Blinds.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tuya.Blinds.dll"]