module PolicyServer.Program
open System
open Suave
open Suave.Filters
open Suave.Operators
open Suave.RequestErrors
open ApiSerialization
open ErrorModels
open HttpUtil

[<EntryPoint>]
let main argv =
    let app : WebPart =
        choose [
            GET >=> choose [ 
                pathScan "/policy/%s/activity" <| fun policyId -> (ok { error = "could find route"; statusCode = 200 })
            ]                       
            (json NOT_FOUND { error = "could not find route"; statusCode = 404 })
        ]
    
    let config = { defaultConfig with bindings = [HttpBinding.createSimple Protocol.HTTP "0.0.0.0" 3002] }            
    startWebServer config app
    0


