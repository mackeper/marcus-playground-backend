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
import Api (api, server)

newtype Visits = Visits {visits :: Int} deriving (Generic, Show)
instance ToJSON Visits

startApp :: Int -> IO ()
startApp port = run port app

app :: Application
app = serve api server
