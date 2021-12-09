#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IotHomeAssistant.Blazor/IotHomeAssistant.Blazor.csproj", "IotHomeAssistant.Blazor/"]
COPY ["IoTHomeAssistant.Domain/IoTHomeAssistant.Domain.csproj", "IoTHomeAssistant.Domain/"]
COPY ["IoTHomeAssistant.Infrastructure/IoTHomeAssistant.Infrastructure.csproj", "IoTHomeAssistant.Infrastructure/"]
RUN dotnet restore "IotHomeAssistant.Blazor/IotHomeAssistant.Blazor.csproj"
COPY . .
WORKDIR "/src/IotHomeAssistant.Blazor"
RUN dotnet build "IotHomeAssistant.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IotHomeAssistant.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IotHomeAssistant.Blazor.dll"]