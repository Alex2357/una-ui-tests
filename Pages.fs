module Pages

open CanopyExtensions
open Common
open canopy.classic


type Home =
    static member uri = Common.baseuri



type SignupPage = 
    static member uri = sprintf "%s/page/create-account" Common.baseuri        
    //member this.Signup = GetEmails this

let signup emailSettings = 
    goto SignupPage.uri
    Signup._accountName << emailSettings.SignupModel.AccountName
    Signup._email << emailSettings.SignupModel.EmailAddress
    Signup._password << emailSettings.SignupModel.Password
    click Signup._submitButton
