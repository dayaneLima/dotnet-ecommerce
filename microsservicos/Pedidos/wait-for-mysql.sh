#!/bin/sh

# Variáveis de ambiente
MYSQL_HOST="mysql-pedidos"  # Nome do serviço do MySQL no Docker Compose
MYSQL_PORT="3306"   # Porta do MySQL
WAIT_TIMEOUT=30  # Tempo limite de espera em segundos

# Função para verificar a conectividade com o MySQL
wait_for_mysql() {
    echo "Aguardando o MySQL iniciar..."
    if nc -z "$MYSQL_HOST" "$MYSQL_PORT" >/dev/null 2>&1; then
        echo "O MySQL está pronto para aceitar conexões."
    else
        echo "Aguardando MySQL..."
        timeout $WAIT_TIMEOUT sh -c 'until nc -z "$0" "$1"; do sleep 1; done' "$MYSQL_HOST" "$MYSQL_PORT" || {
            echo "Tempo limite de espera atingido. O MySQL não está disponível."
            exit 1
        }
        echo "O MySQL está pronto para aceitar conexões."
    fi
}

# Chamando a função para esperar o MySQL
wait_for_mysql

# Executar o comando fornecido como argumento
exec "$@"
