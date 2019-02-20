module EmailCatcher

open System
open MailKit.Net.Imap
open MailKit.Search
open MailKit
open Retry
open System.Linq
open MailKit.Security

type ImapDetails={ Host:string; Port: int; UserName: string; Password:string}

type EmailCatcher={ StartDate: DateTime; ImapDetails: ImapDetails}

let createEmailCatcher imapDetails =
    { StartDate = DateTime.Now; ImapDetails = imapDetails }

let getEmail emailCatcher timeoutSeconds subjectContains toContains = 
    use client = new ImapClient()   
    let imap = emailCatcher.ImapDetails;
    client.ServerCertificateValidationCallback <- fun s c h e -> true;
    client.Connect(imap.Host, imap.Port, SecureSocketOptions.Auto)
    //if fails on next line goto gmail settings, open tab "Forwarding and POP/IMAP" and enable IMAP
    //for some reason fails even after enabled with error: "Invalid credentials (Failure)"
    client.Authenticate (imap.UserName, imap.Password);
  
    let query = SearchQuery.DeliveredAfter(emailCatcher.StartDate)
                    .And(SearchQuery.ToContains(toContains))
                    .And(SearchQuery.SubjectContains (subjectContains))
                    .And(SearchQuery.NotSeen)
    let inbox = client.Inbox
    inbox.Open (FolderAccess.ReadOnly) |> ignore
    let id = retry 1000 timeoutSeconds (fun() -> inbox.Search(query).First())
    let msg = inbox.GetMessage (id);
    client.Disconnect true
    msg

type EmailCatcher with
    member this.GetEmail = getEmail this     

