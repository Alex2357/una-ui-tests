module Common
open EmailCatcher
open System


type SignupModel = { AccountName: string; EmailAddress:string; Password: string}

type EmailSettings = {SignupModel: SignupModel; ImapDetails: ImapDetails}

let baseuri = "https://ci.una.io/test"
let emailPassword = ""
let gmailPassword = ""


let createEmailSettings suffix (date:DateTime) = 
    let suffixWithDate = sprintf "%s-%s" suffix (date.ToString "yyyyMMdd-hhmmss")
    let accountName = sprintf "a-%s" suffixWithDate
    let emailAddress = sprintf "v01234567890123456789012345670%s@gmail.com" 
    let registrationEmail = emailAddress (sprintf "+%s" suffixWithDate)
    {
        SignupModel = { AccountName = accountName; EmailAddress = registrationEmail; Password = emailPassword};
        ImapDetails = { Host="imap.gmail.com"; Port = 993; UserName = emailAddress ""; Password = gmailPassword}
    }


