FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Sensor.Api/Sensor.Api.csproj", "Sensor.Api/"]
RUN dotnet restore "Sensor.Api/Sensor.Api.csproj"
COPY . .
WORKDIR "/src/Sensor.Api"
RUN dotnet build "Sensor.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Sensor.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sensor.Api.dll"]