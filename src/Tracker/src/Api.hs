{-# LANGUAGE DataKinds #-}
{-# LANGUAGE ImportQualifiedPost #-}
{-# LANGUAGE TypeOperators #-}

module Api (API, api, server) where

import Servant
import Visits.Api qualified (API, server)

type API =
    "bye" :> Get '[JSON] String
        :<|> "hello" :> Get '[JSON] String
        :<|> Visits.Api.API

api :: Proxy API
api = Proxy

server :: String -> Server API
server dbpath = do
    return "Bye!"
        :<|> return "Hello!"
        :<|> Visits.Api.server dbpath
