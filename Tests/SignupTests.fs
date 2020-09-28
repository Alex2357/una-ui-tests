module SignupTests


open CanopyExtensions
open canopy.runner.classic
open canopy.classic
open Common
open System
open Pages
open EmailCatcher
open Swensen.Unquote.Assertions

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
        // The first important thing is to verify that we're in the portal
        // and after all required checks are completed we do an attempt to get email.
        // Email delivery is very unstable with gmail it might take time and it is better to
        // setup debug smtp/imap server hope it would be much more reliable
        ProfileHome._profileUserName == settings.SignupModel.AccountName
        let email = catcher.GetEmail 120 settings.SignupModel.AccountName settings.SignupModel.EmailAddress
        test <@ email.HtmlBody.Contains "una" @>
        
// use  dotnet add package HtmlAgilityPack --version 1.9.0 
// to extract anchor from email
        
        
        
