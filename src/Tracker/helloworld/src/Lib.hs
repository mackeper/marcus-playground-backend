{-# LANGUAGE DataKinds #-}

module Lib (startApp, app) where

import Api (api, server)
import Network.Wai.Handler.Warp (run)
import Servant (Application, serve)

startApp :: Int -> IO ()
startApp port = run port app

app :: Application
app = serve api server
