FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

EXPOSE 8080

# Copiar arquivos do projeto e restaurar pacotes
COPY . .
RUN dotnet restore

# Cria o diretório necessário para uploads
RUN mkdir -p /app/upload/avatar

# Publicar a aplicação
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "JobsCalc.dll"]
