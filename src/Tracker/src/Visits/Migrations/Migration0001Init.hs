{-# LANGUAGE OverloadedStrings #-}

module Visits.Migrations.Migration0001Init (migrate) where

import Database.SQLite.Simple (execute_, withConnection)

initializeDatabase :: FilePath -> IO ()
initializeDatabase dbPath = do
  withConnection dbPath $ \conn -> do
    execute_ conn "CREATE TABLE IF NOT EXISTS visit(id INTEGER NOT NULL PRIMARY KEY, url TEXT, time Datetime2)"

migrate :: FilePath -> IO ()
migrate dbPath = do
  initializeDatabase dbPath
