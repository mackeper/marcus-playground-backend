{-# LANGUAGE OverloadedStrings #-}

module Persistence.Persistence
    ( initializeDatabase
    ) where

import Database.SQLite.Simple

initializeDatabase :: FilePath -> IO ()
initializeDatabase dbPath = do
    conn <- open dbPath
    execute_ conn "CREATE TABLE IF NOT EXISTS visits (count INTEGER)"
    execute_ conn "INSERT INTO visits (count) VALUES (0)"
    close conn
