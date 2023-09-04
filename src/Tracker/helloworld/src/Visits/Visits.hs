{-# LANGUAGE DeriveGeneric #-}

module Visits.Visits (Visits, createVisits, incrementVisitCount, getVisitCount) where

import Data.Aeson (FromJSON, ToJSON)
import Database.SQLite.Simple (FromRow (fromRow), field)
import GHC.Generics

newtype Visits = Visits
  { count :: Int
  }
  deriving (Generic, Show, Eq)

instance ToJSON Visits

instance FromJSON Visits

instance FromRow Visits where
  fromRow = Visits <$> field

createVisits :: Visits
createVisits = Visits {count = 0}

incrementVisitCount :: Visits -> Visits
incrementVisitCount visits = visits {count = count visits + 1}

getVisitCount :: Visits -> Int
getVisitCount = count
