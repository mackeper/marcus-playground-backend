{-# LANGUAGE DeriveGeneric #-}

module Visits.Visit (Visit, createVisit) where

import Data.Aeson (FromJSON, ToJSON)
import Data.Time
import Database.SQLite.Simple (FromRow (fromRow), ToRow (toRow), field)
import GHC.Generics

data Visit = Visit
    { id :: Int
    , url :: String
    , time :: UTCTime
    }
    deriving (Generic, Read, Show, Eq)

instance ToJSON Visit
instance FromJSON Visit
instance FromRow Visit where
    fromRow = Visit <$> field <*> field <*> field
instance ToRow Visit where
    toRow (Visit _id url time) = toRow (url, time)

createVisit :: String -> UTCTime -> Visit
createVisit url time = Visit{Visits.Visit.id = 0, url = url, time = time}
