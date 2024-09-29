FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copie os arquivos .csproj e restaure as dependências
COPY ["Example.MassTransit.Consumer/Example.MassTransit.Consumer.csproj", "Example.MassTransit.Consumer/"]
COPY ["Example.MassTransit.Contracts/Example.MassTransit.Contracts.csproj", "Example.MassTransit.Contracts/"]

RUN dotnet restore "Example.MassTransit.Consumer/Example.MassTransit.Consumer.csproj"

# Copie todo o código-fonte
COPY . .

# Publique o aplicativo
WORKDIR "/app/Example.MassTransit.Consumer"
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build-env /app/Example.MassTransit.Consumer/out .

ENTRYPOINT ["dotnet", "Example.MassTransit.Consumer.dll"]
