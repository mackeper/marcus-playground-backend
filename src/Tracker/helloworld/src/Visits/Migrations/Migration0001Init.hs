{-# LANGUAGE OverloadedStrings #-}

module Visits.Migrations.Migration0001Init (migrate) where

import Database.SQLite.Simple
  ( execute_,
    withConnection,
  )

initializeDatabase :: FilePath -> IO ()
initializeDatabase dbPath = do
  withConnection dbPath $ \conn -> do
    execute_ conn "CREATE TABLE IF NOT EXISTS visits (count INTEGER)"

migrate :: FilePath -> IO ()
migrate dbPath = do
  initializeDatabase dbPath
