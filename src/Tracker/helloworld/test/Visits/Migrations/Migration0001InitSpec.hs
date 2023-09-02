module Visits.Migrations.Migration0001InitSpec (spec) where

import Test.Hspec
import System.Directory
import Visits.Migrations.Migration0001Init (migrate)

spec :: Spec
spec = do
    describe "migrate" $ do
        it "Should create a table and insert a row if no rows exist" $ do
            let testDbPath = "test.db"

            migrate testDbPath

            -- Clean up the test database
            removeFile testDbPath
