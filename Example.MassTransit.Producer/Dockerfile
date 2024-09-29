FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copie os arquivos .csproj e restaure as dependências
COPY ["Example.MassTransit.Producer/Example.MassTransit.Producer.csproj", "Example.MassTransit.Producer/"]
COPY ["Example.MassTransit.Contracts/Example.MassTransit.Contracts.csproj", "Example.MassTransit.Contracts/"]

RUN dotnet restore "Example.MassTransit.Producer/Example.MassTransit.Producer.csproj"

# Copie todo o código-fonte
COPY . .

# Publique o aplicativo
WORKDIR "/app/Example.MassTransit.Producer"
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build-env /app/Example.MassTransit.Producer/out .

ENTRYPOINT ["dotnet", "Example.MassTransit.Producer.dll"]
