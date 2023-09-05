{-# LANGUAGE DataKinds #-}
{-# LANGUAGE TypeOperators #-}

module Visits.Api (API, server, getVisits, postVisits) where

import Servant
import Visits.Visits (
    Visits,
    createVisits,
    getVisitCount,
    incrementVisitCount,
 )

type API = "visits" :> Get '[JSON] Visits :<|> "visits" :> Post '[JSON] Visits

server :: Server API
server = getVisits :<|> postVisits

getVisits :: Handler Visits
getVisits = return createVisits

postVisits :: Handler Visits
postVisits = return $ incrementVisitCount createVisits
