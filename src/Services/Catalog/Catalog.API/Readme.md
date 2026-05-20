# PARA EXECUTAR COMANDOS DOCKER, LEMBRE-SE DE ESTAR NO DIRETÓRIO /src

# para executar o docker compose, execute o comando abaixo na raiz do projeto:

## para parar as imagens
docker compose down -v

## para subir as imagens
docker compose up -d

# se aparecer essis como esses ao tentar subir o docker desktop:
Docker Desktop - 
Remaining Processes Some remaining processes cannot be terminated: Docker Desktop.exe
* pid 26728: Docker Desktop.exe

- mate todos os processos que estiverem rodando no gerenciado de tarefas
- execute esse comando no poweshell como admin: wsl --shutdown
- espere 5s e tente subir o docker desktop novamente

# criando/atualizando dockerfile
- botao direito no projeto > Add > dockersupport > Linux
- irá gerar o aquivo e nao ha necessidade de alterá-lo

# docker-compose
- botao direito no projeto > Add > container orchestrator support > docker compose > add
- irá gerar o aquivo e o override, mas precisa ser atualizado para ser executado corretamente
- 
# healthcheck - postgres
# para validar a saude do banco de dados postgres foi incluso essa lib
"AspNetCore.HealthChecks.NpgSql" Version="8.0.0"
"AspNetCore.HealthChecks.UI.Client" Version="8.0.0"

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

