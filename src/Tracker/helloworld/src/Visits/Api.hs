{-# LANGUAGE DataKinds #-}
{-# LANGUAGE TypeOperators #-}
{-# LANGUAGE DeriveGeneric #-}

module Visits.Api (VisitsAPI, visitsServer, getVisits, postVisits) where

import Servant
import Network.Wai.Handler.Warp (run)
import Data.Aeson (ToJSON)
import GHC.Generics

newtype Visits = Visits {visits :: Int} deriving (Generic, Show)
instance ToJSON Visits

type VisitsAPI
    = "visits" :> Get '[JSON] Visits
    :<|> "visits" :> Post '[JSON] Visits

visitsServer :: Server VisitsAPI
visitsServer
    = getVisits
    :<|> postVisits

getVisits :: Handler Visits
getVisits = return $ Visits 100

postVisits :: Handler Visits
postVisits = return $ Visits 101
