{-# LANGUAGE DataKinds #-}
{-# LANGUAGE TypeOperators #-}

module Api (API, api, server) where
import Servant
import qualified Visits.Api (API, server)

type API
    = "bye" :> Get '[JSON] String
    :<|> "hello" :> Get '[JSON] String
    :<|> Visits.Api.API

api :: Proxy API
api = Proxy

server :: Server API
server
    = return "Bye!"
    :<|> return "Hello!"
    :<|> Visits.Api.server
