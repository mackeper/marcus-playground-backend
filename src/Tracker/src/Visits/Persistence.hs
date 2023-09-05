{-# LANGUAGE ImportQualifiedPost #-}
{-# LANGUAGE OverloadedStrings #-}

module Visits.Persistence (
  migrate,
  getVisitCount,
  incrementVisitCount,
)
where

import Database.SQLite.Simple (
  execute_,
  query_,
  withConnection,
 )
import Visits.Migrations.Migration0001Init qualified (migrate)
import Visits.Visits (Visits)

migrate :: FilePath -> IO ()
migrate dbPath = do
  Visits.Migrations.Migration0001Init.migrate dbPath

getVisitCount :: FilePath -> IO Int
getVisitCount dbPath = do
  withConnection dbPath $ \conn -> do
    r <- query_ conn "SELECT * FROM visits" :: IO [Visits]
    case r of
      [] -> return 0
      x -> return $ length x

incrementVisitCount :: FilePath -> IO ()
incrementVisitCount dbPath = do
  withConnection dbPath $ \conn -> do
    execute_ conn "INSERT INTO visits (count) VALUES (0)"
