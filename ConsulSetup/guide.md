### Setup Consul for dev

Note for often usage command

`docker pull consul:latest`

`docker run -d -p 8500:8500 -p 8600:8600/udp --name=dev-consul consul agent -server -ui -node=server-1 -bootstrap-expect=1 -bind="0.0.0.0" -client="0.0.0.0"`

`docker cp ConsulSettingFile.json dev-consul:/consul/config/ProductService.json`

`docker exec -it dev-consul sh`

`consul reload`

`docker run -d --net="host" --name=dev-consul consul agent -server -ui -node=server-1 -bootstrap-expect=1`

1. Start Docker with Docker-Compose
2. Upload file ProductService.json to dev-consul:/consul/config/
3. Exen "consul reload" to reload services
