module Main (main) where

import Lib (startApp)
import Visits.Persistence (
    getVisitCount,
    incrementVisitCount,
    migrate,
 )

import Data.Time
import Visits.Visit (createVisit)

setupDatabase :: String -> IO ()
setupDatabase path = do
    migrate path

    visit1 <- getVisitCount path
    putStrLn $ "Current visit count: " ++ show visit1

    _ <- incrementVisitCount path (createVisit "https://realmoneycompany.com" UTCTime{utctDay = fromGregorian 2023 1 1, utctDayTime = timeOfDayToTime (TimeOfDay 0 0 0)})

    visit2 <- getVisitCount path
    putStrLn $ "Current visit count: " ++ show visit2

main :: IO ()
main = do
    let port = 5002
    let dbPath = "tracker.db"

    setupDatabase dbPath

    putStrLn $ "Starting server on port " ++ show port ++ "..."
    startApp port dbPath
