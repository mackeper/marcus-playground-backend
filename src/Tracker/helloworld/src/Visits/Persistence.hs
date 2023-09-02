{-# LANGUAGE OverloadedStrings #-}

module Visits.Persistence
  ( Visits.Persistence.migrate,
    getVisitCount,
    incrementVisitCount,
  )
where

import Database.SQLite.Simple
  ( Only (Only),
    execute_,
    query_,
    withConnection,
  )

import Visits.Migrations.Migration0001Init (migrate)

migrate :: FilePath -> IO ()
migrate dbPath = do
  Visits.Migrations.Migration0001Init.migrate dbPath

getVisitCount :: FilePath -> IO Int
getVisitCount dbPath = do
  withConnection dbPath $ \conn -> do
    r <- query_ conn "SELECT count FROM visits"
    case r of
      [] -> return 0
      (Only count) : _ -> return count

incrementVisitCount :: FilePath -> IO ()
incrementVisitCount dbPath = do
  withConnection dbPath $ \conn -> do
    execute_ conn "UPDATE visits SET count = count + 1"
