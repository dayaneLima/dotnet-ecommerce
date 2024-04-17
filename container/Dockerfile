# Informa ao Docker qual container iremos utilizar
FROM ubuntu:24.04 as base

# Instalacao libs necessarias
RUN apt-get update
RUN apt-get install -y curl wget apt-utils apt-transport-https lsb-release gnupg dirmngr
RUN apt-get install -y lsof nano
RUN apt-get install -y libc6 libgcc-s1 liblttng-ust1 libssl3 libstdc++6 libunwind8 zlib1g

# Instalacao .net
RUN apt-get update && apt-get install -y dotnet-sdk-8.0

# Instalação da ferramente do ef para migrations e adição na variável de sistema
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Instalacao tzdata
ENV TZ=America/Recife
RUN apt-get update && apt-get install -y tzdata

# CMD tail -f /dev/null

# Cria o diretório codes na raiz do container
RUN mkdir -p /app

# Define o diretório codes como diretório de trabalho
# Isso vai espelhar a pasta codes no diretório debian-core para a pasta codes no container
WORKDIR /app


# Ambiente de desenvolvimento
FROM base as development

COPY . .
WORKDIR /app

# Restaurar dependências e compilar o código
RUN dotnet restore && dotnet build