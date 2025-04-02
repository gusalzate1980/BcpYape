# Usa la imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Usa la imagen de compilación de .NET
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Service1/Service1.csproj", "Service1/"]
COPY ["Service2/Service2.csproj", "Service2/"]
RUN dotnet restore "Service1/Service1.csproj"
RUN dotnet restore "Service2/Service2.csproj"
COPY . .
WORKDIR "/src/Service1"
RUN dotnet build "Service1.csproj" -c Release -o /app/build
WORKDIR "/src/Service2"
RUN dotnet build "Service2.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/Service1"
RUN dotnet publish "Service1.csproj" -c Release -o /app/publish
WORKDIR "/src/Service2"
RUN dotnet publish "Service2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AntiFraud.dll"]