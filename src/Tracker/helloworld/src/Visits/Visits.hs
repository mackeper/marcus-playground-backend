{-# LANGUAGE DeriveGeneric #-}

module Visits.Visits (Visits) where

import Data.Aeson (ToJSON)
import GHC.Generics

newtype Visits = Visits {visits :: Int} deriving (Generic, Show)
instance ToJSON Visits
