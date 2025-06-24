# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos solo el .csproj desde su subcarpeta
COPY UaddAPI/UaddAPI.csproj ./UaddAPI/
RUN dotnet restore "UaddAPI/UaddAPI.csproj"

# Copiamos todo el proyecto
COPY . .
WORKDIR /src/UaddAPI
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "UaddAPI.dll"]
