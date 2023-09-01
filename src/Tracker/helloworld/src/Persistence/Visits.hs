{-# LANGUAGE OverloadedStrings #-}

module Persistence.Visits
    ( initializeDatabase
    , getVisitCount
    , incrementVisitCount
    ) where

import Database.SQLite.Simple
    ( execute_, query_, Only(Only), withConnection )

initializeDatabase :: FilePath -> IO ()
initializeDatabase dbPath = do
    withConnection dbPath $ \conn -> do
        execute_ conn "CREATE TABLE IF NOT EXISTS visits (count INTEGER)"
        existingVisits <- query_ conn "SELECT count FROM visits" :: IO [Only Int]
        case existingVisits of
            [] -> execute_ conn "INSERT INTO visits (count) VALUES (0)"
            _ -> return ()

getVisitCount :: FilePath -> IO Int
getVisitCount dbPath = do
    initializeDatabase dbPath
    withConnection dbPath $ \conn -> do
        r <- query_ conn "SELECT count FROM visits"
        case r of
            [] -> return 0
            (Only count):_ -> return count

incrementVisitCount :: FilePath -> IO ()
incrementVisitCount dbPath = do
    initializeDatabase dbPath
    withConnection dbPath $ \conn -> do
        execute_ conn "UPDATE visits SET count = count + 1"
