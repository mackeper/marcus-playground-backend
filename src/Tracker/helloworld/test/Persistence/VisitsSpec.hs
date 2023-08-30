module Persistence.VisitsSpec (spec) where

import Test.Hspec
import System.Directory
import Persistence.Visits (initializeDatabase, getVisitCount, incrementVisitCount)

spec :: Spec
spec = do
    describe "initializeDatabase" $ do
        it "Should create a table and insert a row if no rows exist" $ do
            let testDbPath = "test.db"

            existingVisits <- getVisitCount testDbPath
            existingVisits `shouldBe` 0

            -- Clean up the test database
            removeFile testDbPath

    describe "getVisitCount" $ do
        it "Should get the visit count" $ do
            let testDbPath = "test.db"

            existingVisits <- getVisitCount testDbPath
            existingVisits `shouldBe` 0

            -- Clean up the test database
            removeFile testDbPath
    
    describe "incrementVisitCount" $ do
        it "Should increment the visit count" $ do
            let testDbPath = "test.db"

            existingVisits <- getVisitCount testDbPath
            existingVisits `shouldBe` 0

            incrementVisitCount testDbPath
            existingVisits2 <- getVisitCount testDbPath
            existingVisits2 `shouldBe` 1

            -- Clean up the test database
            removeFile testDbPath
