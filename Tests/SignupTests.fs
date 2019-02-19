module SignupTests


open CanopyExtensions
open canopy.runner.classic
open canopy.classic
open Common
open System
open Pages
open EmailCatcher

let all () =

    context "Signup tests"  

    before (fun _ -> 
        goto Pages.SignupPage.uri
    )

    "Account Name not entered shows error required field" &&& fun _ ->        
        click Signup._submitButton
        Signup._accountNameError == "This is a required field."

    "Signup" &&&& fun _ ->       
        let settings = createEmailSettings "uitestsuna" DateTime.Now
        let catcher = createEmailCatcher settings.ImapDetails
        Pages.signup settings
        
        let email = catcher.GetEmail 120 settings.SignupModel.AccountName settings.SignupModel.EmailAddress
        let contains = email.HtmlBody.Contains "una"
        ProfileHome._profileUserName == settings.SignupModel.AccountName
// use  dotnet add package HtmlAgilityPack --version 1.9.0 
// to extract anchor
        
        
        
