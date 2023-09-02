{-# LANGUAGE DataKinds #-}
{-# LANGUAGE TypeOperators #-}
{-# LANGUAGE DeriveGeneric #-}

module Visits.Api (API, server, getVisits, postVisits) where

import Servant
import Data.Aeson (ToJSON)
import GHC.Generics

newtype Visits = Visits {visits :: Int} deriving (Generic, Show)
instance ToJSON Visits

type API
    = "visits" :> Get '[JSON] Visits
    :<|> "visits" :> Post '[JSON] Visits

server :: Server API
server
    = getVisits
    :<|> postVisits

getVisits :: Handler Visits
getVisits = return $ Visits 100

postVisits :: Handler Visits
postVisits = return $ Visits 101
