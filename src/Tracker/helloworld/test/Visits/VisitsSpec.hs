{-# LANGUAGE OverloadedStrings #-}

module Visits.VisitsSpec (spec) where

import Data.Aeson (decode, encode)
import Test.Hspec
import Visits.Visits (Visits, createVisits, getVisitCount, incrementVisitCount)

spec :: Spec
spec = do
  describe "Visits" $ do
    it "create should have 0 count" $ do
      let visits = createVisits
      getVisitCount visits `shouldBe` 0
    it "increment should add 1 to count" $ do
      let visits1 = createVisits
      let visits2 = incrementVisitCount visits1
      let visits3 = incrementVisitCount visits2
      getVisitCount visits1 `shouldBe` 0
      getVisitCount visits2 `shouldBe` 1
      getVisitCount visits3 `shouldBe` 2
    it "ToJson" $ do
      let visits = createVisits
      getVisitCount visits `shouldBe` 0
      encode visits `shouldBe` "{\"count\":0}"
    it "FromJson" $ do
      let visits = createVisits
      let result = decode "{\"count\":0}" :: Maybe Visits
      result `shouldBe` Just visits
