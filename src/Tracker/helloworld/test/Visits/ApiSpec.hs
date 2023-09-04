{-# LANGUAGE OverloadedStrings #-}

module Visits.ApiSpec (spec) where

import Lib (app)
import Test.Hspec
import Test.Hspec.Wai

spec :: Spec
spec = with (return app) $ do
  describe "GET /visits" $ do
    it "responds with 200" $ do
      get "/visits" `shouldRespondWith` 200
    it "responds with json" $ do
      get "/visits" `shouldRespondWith` "{\"count\":0}"
  describe "POST /visits" $ do
    it "responds with 200" $ do
      post "/visits" "" `shouldRespondWith` 200
    it "responds with json" $ do
      post "/visits" "" `shouldRespondWith` "{\"count\":1}"
