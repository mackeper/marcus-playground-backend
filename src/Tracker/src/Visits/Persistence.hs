{-# LANGUAGE ImportQualifiedPost #-}
{-# LANGUAGE OverloadedStrings #-}

module Visits.Persistence (
  migrate,
  getVisitCount,
  incrementVisitCount,
  getVisits,
)
where

import Data.Time
import Database.SQLite.Simple (
  execute,
  query_,
  withConnection,
 )
import Visits.Migrations.Migration0001Init qualified (migrate)
import Visits.Visit (Visit, createVisit)

migrate :: FilePath -> IO ()
migrate dbPath = do
  Visits.Migrations.Migration0001Init.migrate dbPath

getVisitCount :: FilePath -> IO Int
getVisitCount dbPath = do
  withConnection dbPath $ \conn -> do
    r <- query_ conn "SELECT * FROM visit" :: IO [Visit]
    case r of
      [] -> return 0
      x -> return $ length x

getVisits :: FilePath -> IO [Visit]
getVisits dbPath = do
  withConnection dbPath $ \conn -> do
    query_ conn "SELECT * FROM visit" :: IO [Visit]

incrementVisitCount :: FilePath -> Visit -> IO (Maybe Visit)
incrementVisitCount dbPath visit = do
  withConnection dbPath $ \conn -> do
    execute conn "INSERT INTO visit (url, time) VALUES (?, ?)" visit
    newVisit <- query_ conn "SELECT * FROM visit WHERE id = (SELECT MAX(id) FROM visit)" :: IO [Visit]
    return $ case newVisit of
      [x] -> Just x
      _ -> Nothing
