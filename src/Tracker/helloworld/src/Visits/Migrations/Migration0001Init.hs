{-# LANGUAGE OverloadedStrings #-}

module Visits.Migrations.Migration0001Init (migrate) where

import Database.SQLite.Simple
  ( Only,
    execute_,
    query_,
    withConnection,
  )

initializeDatabase :: FilePath -> IO ()
initializeDatabase dbPath = do
    withConnection dbPath $ \conn -> do
        execute_ conn "CREATE TABLE IF NOT EXISTS visits (count INTEGER)"
        existingVisits <- query_ conn "SELECT count FROM visits" :: IO [Only Int]
        case existingVisits of
            [] -> execute_ conn "INSERT INTO visits (count) VALUES (0)"
            _ -> return ()

migrate :: FilePath -> IO ()
migrate dbPath = do
    initializeDatabase dbPath
