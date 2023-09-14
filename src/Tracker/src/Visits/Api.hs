{-# LANGUAGE DataKinds #-}
{-# LANGUAGE ImportQualifiedPost #-}
{-# LANGUAGE TypeOperators #-}

module Visits.Api (API, server, getVisits, postVisits) where

import Control.Monad.IO.Class
import Servant
import Visits.Persistence qualified (
    getVisits,
    incrementVisitCount,
 )
import Visits.Visit (Visit)

type API =
    "visits" :> Get '[JSON] [Visit]
        :<|> "visits" :> ReqBody '[JSON] Visit :> Post '[JSON] (Maybe Visit)

server :: FilePath -> Server API
server dbpath = getVisits dbpath :<|> postVisits dbpath

getVisits :: FilePath -> Handler [Visit]
getVisits dbpath = liftIO $ Visits.Persistence.getVisits dbpath

postVisits :: FilePath -> Visit -> Handler (Maybe Visit)
postVisits dbpath visit = liftIO $ Visits.Persistence.incrementVisitCount dbpath visit
