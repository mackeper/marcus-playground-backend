module Main (main) where

import Lib (startApp)
import Visits.Persistence (
    getVisitCount,
    incrementVisitCount,
    migrate,
 )

setupDatabase :: String -> IO ()
setupDatabase path = do
    migrate path

    visit1 <- getVisitCount path
    putStrLn $ "Current visit count: " ++ show visit1

    incrementVisitCount path

    visit2 <- getVisitCount path
    putStrLn $ "Current visit count: " ++ show visit2

main :: IO ()
main = do
    let port = 5002
    let dbPath = "tracker.db"

    setupDatabase dbPath

    putStrLn $ "Starting server on port " ++ show port ++ "..."
    startApp port
