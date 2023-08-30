{-# LANGUAGE OverloadedStrings #-}

module Persistence.Visits
    ( initializeDatabase
    , getVisitCount
    , incrementVisitCount
    ) where

import Database.SQLite.Simple

initializeDatabase :: FilePath -> IO ()
initializeDatabase dbPath = do
    conn <- open dbPath
    execute_ conn "CREATE TABLE IF NOT EXISTS visits (count INTEGER)"
    existingVisits <- query_ conn "SELECT count FROM visits" :: IO [Only Int]
    case existingVisits of
        [] -> execute_ conn "INSERT INTO visits (count) VALUES (0)"
        _ -> return ()
    close conn

getVisitCount :: FilePath -> IO Int
getVisitCount dbPath = do
    initializeDatabase dbPath
    conn <- open dbPath
    r <- query_ conn "SELECT count FROM visits"
    close conn
    case r of
        [] -> return 0
        (Only count):_ -> return count

incrementVisitCount :: FilePath -> IO ()
incrementVisitCount dbPath = do
    initializeDatabase dbPath
    conn <- open dbPath
    execute_ conn "UPDATE visits SET count = count + 1"
    close conn
