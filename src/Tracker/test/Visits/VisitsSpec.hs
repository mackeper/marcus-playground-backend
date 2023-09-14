{-# LANGUAGE OverloadedStrings #-}

module Visits.VisitsSpec (spec) where

import Data.Aeson (decode, encode)
import Data.Time (
    TimeOfDay (TimeOfDay),
    UTCTime (..),
    fromGregorian,
    timeOfDayToTime,
 )
import Test.Hspec (Spec, describe, it, shouldBe)
import Visits.Visit (
    Visit,
    createVisit,
 )

url :: String
url = "https://realmoneycompany.com"

time :: UTCTime
time = UTCTime{utctDay = fromGregorian 2023 1 1, utctDayTime = timeOfDayToTime (TimeOfDay 0 0 0)}

spec :: Spec
spec = do
    describe "Visit" $ do
        it "ToJson" $ do
            let visit = createVisit url time
            encode visit `shouldBe` "{\"id\":0,\"time\":\"2023-01-01T00:00:00Z\",\"url\":\"https://realmoneycompany.com\"}"
        it "FromJson" $ do
            let visit = createVisit url time
            let result = decode "{\"id\":0, \"url\":\"https://realmoneycompany.com\", \"time\":\"2023-01-01T00:00:00Z\"}" :: Maybe Visit
            result `shouldBe` Just visit
