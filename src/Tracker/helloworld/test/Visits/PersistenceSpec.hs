module Visits.PersistenceSpec (spec) where

import Test.Hspec
import System.Directory
import Visits.Persistence (migrate, getVisitCount, incrementVisitCount)

spec :: Spec
spec = do
    describe "migrate all" $ do
        it "Run all migrations" $ do
            let testDbPath = "test.db"
            migrate testDbPath

            existingVisits <- getVisitCount testDbPath
            existingVisits `shouldBe` 0

            -- Clean up the test database
            removeFile testDbPath

    describe "getVisitCount" $ do
        it "Should get the visit count" $ do
            let testDbPath = "test.db"
            migrate testDbPath

            existingVisits <- getVisitCount testDbPath
            existingVisits `shouldBe` 0

            -- Clean up the test database
            removeFile testDbPath
    
    describe "incrementVisitCount" $ do
        it "Should increment the visit count" $ do
            let testDbPath = "test.db"
            migrate testDbPath

            existingVisits <- getVisitCount testDbPath
            existingVisits `shouldBe` 0

            incrementVisitCount testDbPath
            existingVisits2 <- getVisitCount testDbPath
            existingVisits2 `shouldBe` 1

            -- Clean up the test database
            removeFile testDbPath
