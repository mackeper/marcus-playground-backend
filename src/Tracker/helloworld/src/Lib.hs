{-# LANGUAGE DataKinds #-}
{-# LANGUAGE TypeOperators #-}
{-# LANGUAGE DeriveGeneric #-}

module Lib
    ( startApp
    ) where

import Servant
import Network.Wai.Handler.Warp (run)
import Data.Aeson (ToJSON)
import GHC.Generics

newtype Visits = Visits {visits :: Int} deriving (Generic, Show)
instance ToJSON Visits

type API
    = "visits" :> Get '[JSON] Visits
    :<|> "visits" :> Post '[JSON] Visits
    :<|> "bye" :> Get '[JSON] String


getVisits :: Handler Visits
getVisits = return $ Visits 100

postVisits :: Handler Visits
postVisits = return $ Visits 101

startApp :: Int -> IO ()
startApp port = run port app

app :: Application
app = serve api server

api :: Proxy API
api = Proxy

server :: Server API
server
    = getVisits
    :<|> postVisits
    :<|> return "Bye!"
