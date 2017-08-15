module PolicyServer.HttpUtil
open System.IO
open System.Text
open HttpFs.Client
open HttpFs.Client.Request
open Newtonsoft.Json
open Newtonsoft.Json.Serialization
open Hopac

let private jsonSerializerSettings = JsonSerializerSettings(ContractResolver = CamelCasePropertyNamesContractResolver())

type RestResponse<'Response> = 
    Error of string*int    
    | Success of 'Response

let private jsonContentTypeHeader = 
    RequestHeader.ContentType (ContentType.create("application","json", Encoding.UTF8))

let private mapResponse<'Response> request =
    job {
        let! responseOrErr = tryGetResponse request // disposed at the end of async, don't        
        match responseOrErr with 
        | Choice1Of2 response -> 
            let! responseBodyStr = Response.readBodyAsString response        
            if (response.statusCode/100) = 2 then
                let result = JsonConvert.DeserializeObject<'Response>(responseBodyStr, jsonSerializerSettings)
                return Success result
            else
                return Error (responseBodyStr,response.statusCode)        
        | Choice2Of2 exc -> 
            return Error ((sprintf "%A" exc),0)
    }

let private setResponseHeaders clientId clientSecret =         
    basicAuthentication clientId clientSecret
    >> setHeader (RequestHeader.Accept "application/json")                                        
    >> responseCharacterEncoding Encoding.UTF8            

let private addJsonBody<'Request> (body:'Request) =     
    bodyStringEncoded (JsonConvert.SerializeObject (body, jsonSerializerSettings)) Encoding.UTF8
    >> setHeader jsonContentTypeHeader  

let private fromJobToAsync job = job |> (Hopac.startAsTask >> Async.AwaitTask)

let postRequest<'Request,'Response> url (body:'Request) clientId clientSecret =                     
    createUrl Post url    
    |> addJsonBody body    
    |> setResponseHeaders clientId clientSecret
    |> mapResponse<'Response>                    
    |> fromJobToAsync

let getRequest<'Response> url clientId clientSecret = 
    createUrl Get url
    |> setResponseHeaders clientId clientSecret
    |> mapResponse<'Response>                    
    |> fromJobToAsync