{-# LANGUAGE DataKinds #-}
{-# LANGUAGE TypeOperators #-}

module Api (API, api, server) where
import Servant
import Visits.Api (VisitsAPI, visitsServer)

type API
    = "bye" :> Get '[JSON] String
    :<|> "hello" :> Get '[JSON] String
    :<|> VisitsAPI

api :: Proxy API
api = Proxy

server :: Server API
server
    = return "Bye!"
    :<|> return "Hello!"
    :<|> visitsServer
