module PolicyServer.ApiSerialization
open Suave
open Suave.Operators
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

let private jsonSerializerSettings = JsonSerializerSettings(ContractResolver = CamelCasePropertyNamesContractResolver())

let json<'T> statusCode (v : 'T) =  
  JsonConvert.SerializeObject(v, jsonSerializerSettings)
  |> statusCode
  >=> Writers.setMimeType "application/json; charset=utf-8"

let ok<'T> = json<'T> Successful.OK