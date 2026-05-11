# PARA EXECUTAR COMANDOS DOCKER, LEMBRE-SE DE ESTAR NO DIRETÓRIO /src

# para executar o docker compose, execute o comando abaixo na raiz do projeto:
docker compose up -d

# para executar comandos no container do postgres siga esse passo a passo:
docker ps # para listar os containers em execução e pegar o nome do container do postgres
copie o container_id
docker exec -it <nome_do_container> bash # para acessar o terminal do container

# lista todos os bancos de dados
\l

# no terminal:
psql -U postgres # para acessar o psql com o usuário postgres

\c CatalogDb # para conectar ao banco de dados CatalogDb

# lista as tabelas desse banco
\d

