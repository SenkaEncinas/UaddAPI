# Etapa base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UaddAPI/UaddAPI.csproj", "UaddAPI/"]
RUN dotnet restore "UaddAPI/UaddAPI.csproj"
COPY . .
WORKDIR "/src/UaddAPI"
RUN dotnet build "UaddAPI.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "UaddAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UaddAPI.dll"]
