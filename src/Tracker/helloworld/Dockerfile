FROM debian:11-slim

LABEL maintainer="Marcus"
LABEL version="0.1"
LABEL description="Environment for compiling haskell"

ARG DEBIAN_FRONTEND=noninteractive
SHELL [ "/bin/bash", "-c" ]

RUN apt-get update && apt-get install --no-install-recommends -y\
    build-essential curl libffi-dev libffi7 libgmp-dev libgmp10 libncurses-dev libncurses5 libtinfo5 ca-certificates libghc-zlib-dev  libghc-zlib-bindings-dev

RUN curl --proto '=https' --tlsv1.2 -sSf https://get-ghcup.haskell.org | BOOTSTRAP_HASKELL_NONINTERACTIVE=1 sh

ENV PATH="/root/.cabal/bin:/root/.ghcup/bin:${PATH}"
