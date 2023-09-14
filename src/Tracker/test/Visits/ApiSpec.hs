{-# LANGUAGE ImportQualifiedPost #-}
{-# LANGUAGE OverloadedStrings #-}

module Visits.ApiSpec (spec) where

import Data.ByteString qualified
import Data.ByteString.Lazy qualified
import Lib (app)
import Network.HTTP.Types
import Network.Wai.Test
import System.Directory (removeFile)
import Test.Hspec
import Test.Hspec.Wai
import Visits.Persistence (migrate)

postRequest :: Data.ByteString.ByteString -> Data.ByteString.Lazy.ByteString -> WaiSession st SResponse
postRequest url = Test.Hspec.Wai.request methodPost url [("Content-Type", "application/json")]

dbpath :: String
dbpath = "test.db"

spec :: Spec
spec =
    before (migrate dbpath)
        $ after (\_ -> removeFile dbpath)
        $ with (return $ app dbpath)
        $ do
            describe "GET /visits"
                $ do
                    it "responds with 200"
                        $ do
                            get "/visits" `shouldRespondWith` 200
                    it "responds with json"
                        $ do
                            _ <- postRequest "/visits" "{\"id\":0,\"time\":\"2023-01-01T00:00:00Z\",\"url\":\"https://realmoneycompany.com\"}"
                            get "/visits" `shouldRespondWith` "[{\"id\":1,\"time\":\"2023-01-01T00:00:00Z\",\"url\":\"https://realmoneycompany.com\"}]"
            describe "POST /visits"
                $ do
                    it "responds with 200"
                        $ do
                            postRequest
                                "/visits"
                                "{\"id\":0,\"time\":\"2023-01-01T00:00:00Z\",\"url\":\"https://realmoneycompany.com\"}"
                                `shouldRespondWith` 200
                    it "responds with json"
                        $ do
                            postRequest
                                "/visits"
                                "{\"id\":0,\"time\":\"2023-01-01T00:00:00Z\",\"url\":\"https://realmoneycompany.com\"}"
                                `shouldRespondWith` "{\"id\":1,\"time\":\"2023-01-01T00:00:00Z\",\"url\":\"https://realmoneycompany.com\"}"
