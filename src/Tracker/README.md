# Tracker

Stack: Servant API with sqlite-simple persistence.

Keep track on website usage, visits, etc.

## Development :sparkles:

### Setup

Install stack, cabal, ghc using [GHCup](https://www.haskell.org/ghcup/).

### Stack

Using stack to build and test.

| Command     | Effect                               |
| :---------- | :----------------------------------- |
| stack build | Build the project                    |
| stack test  | Run all tests in the project         |
| stack ghci  | Run ghci with the project as context |

### Docker

Using Docker to cross-compile (might be a better way?)

`docker build -t debian11-haskell:v1.0 .`

`docker run --name "haskell-builder" -v "$(pwd)\docker-volume:/docker-volume" -dit debian11-haskell:v1.0`

`docker exec --workdir /docker-volume "haskell-builder" /bin/bash "-c" "stack build"`
