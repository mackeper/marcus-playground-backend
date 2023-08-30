# helloworld

## Docker
`docker build -t debian11-haskell:v1.0 .`  

`docker run --name "haskell-builder" -v "$(pwd)\docker-volume:/docker-volume" -dit debian11-haskell:v1.0`  

`docker exec --workdir /docker-volume "haskell-builder" /bin/bash "-c" "stack build"`  
