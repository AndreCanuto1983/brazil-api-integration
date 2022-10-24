# brazil-api-integration
Simple api to search for cnpj and search for books, integrating with external public api service https://brasilapi.com.br/

Docker Command:

generate image
    docker build -f Brazil.Api.Integration\Dockerfile -t pos.puc.tcc.api .

run container
    docker container run -d -p 49168:80 --name pos.puc.tcc.api pos.puc.tcc.api