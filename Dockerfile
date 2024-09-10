# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /app

# Copia os arquivos do projeto e restaura as dependências
COPY *.sln .
COPY MotoRental.Api/*.csproj ./MotoRental.Api/
COPY MotoRental.Application/*.csproj ./MotoRental.Application/
COPY MotoRental.Infrastructure/*.csproj ./MotoRental.Infrastructure/
COPY MotoRental.Domain/*.csproj ./MotoRental.Domain/

RUN dotnet restore

# Copia todo o código-fonte
COPY . .

# Compila a aplicação
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expõe a porta em que a aplicação vai rodar
EXPOSE 80

# Define o comando para rodar a aplicação
ENTRYPOINT ["dotnet", "MotoRental.Api.dll"]
