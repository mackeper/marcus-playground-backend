{-# LANGUAGE ImportQualifiedPost #-}

module Visits.PersistenceSpec (spec) where

import System.Directory
import Test.Hspec
import Visits.Persistence (
    getVisitCount,
    incrementVisitCount,
    migrate,
 )

import Data.Time
import Visits.Visit qualified (Visit, createVisit)

createVisit :: Visits.Visit.Visit
createVisit = Visits.Visit.createVisit "https://realmoneycompany.com" UTCTime{utctDay = fromGregorian 2023 1 1, utctDayTime = timeOfDayToTime (TimeOfDay 0 0 0)}

dbpath :: String
dbpath = "test.db"

spec :: Spec
spec =
    before (migrate dbpath)
        $ after (\_ -> removeFile dbpath)
        $ do
            describe "migrate all"
                $ do
                    it "Run all migrations"
                        $ do
                            existingVisit <- getVisitCount dbpath
                            existingVisit `shouldBe` 0
            describe "getVisitCount"
                $ do
                    it "Should get the visit count"
                        $ do
                            existingVisit <- getVisitCount dbpath
                            existingVisit `shouldBe` 0
            describe "incrementVisitCount"
                $ do
                    it "Should increment the visit count"
                        $ do
                            existingVisit <- getVisitCount dbpath
                            existingVisit `shouldBe` 0
                            _ <- incrementVisitCount dbpath createVisit
                            existingVisit2 <- getVisitCount dbpath
                            existingVisit2 `shouldBe` 1
