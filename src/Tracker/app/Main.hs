module Main (main) where

import Lib (startApp)
import Visits.Persistence (
    migrate,
 )

setupDatabase :: String -> IO ()
setupDatabase path = do
    migrate path

main :: IO ()
main = do
    let port = 5002
    let dbPath = "tracker.db"

    setupDatabase dbPath

    putStrLn $ "Starting server on port " ++ show port ++ "..."
    startApp port dbPath
