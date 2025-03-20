# Usar una imagen base de .NET SDK para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar los archivos del proyecto y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto del código y compilar la aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Usar una imagen base de .NET Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar los archivos compilados desde la etapa de construcción
COPY --from=build-env /app/out .

# Exponer el puerto 80 (puerto predeterminado para aplicaciones web)
EXPOSE 80

# Definir el punto de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ULEats.dll"]