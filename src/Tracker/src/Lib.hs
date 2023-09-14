{-# LANGUAGE DataKinds #-}

module Lib (startApp, app) where

import Api (api, server)
import Network.Wai.Handler.Warp (run)
import Network.Wai.Middleware.Cors
import Servant (Application, serve)

startApp :: Int -> FilePath -> IO ()
startApp port dbpath = run port $ app dbpath

app :: FilePath -> Application
app dbpath = simpleCors $ serve api $ server dbpath
