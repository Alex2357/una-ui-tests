open System

open canopy
open canopy.classic
open canopy.runner.classic
open types
open configuration
open reporters
open Retry


[<EntryPoint>]
let main _ =
  configuration.wipSleep <- 0.2
  configuration.compareTimeout <- 10.0
  configuration.elementTimeout <- 10.0
  configuration.pageTimeout <- 10.0
  configuration.autoPinBrowserRightOnLaunch <- false
  //configuration.failFast := true


  configuration.chromeDir <- "/usr/bin"
//   configuration.firefoxDriverDir <- "c:\work"
//   configuration.firefoxDir <- @"c:\Users\alex\AppData\Local\Mozilla Firefox\firefox.exe"
//   configuration.showInfoDiv <- false

  reporter <- JUnitReporter("./results.xml") //:> IReporter

  
  let capability = OpenQA.Selenium.Remote.DesiredCapabilities()
  capability.SetCapability("browserName", "chrome")      

  try

    let debugBrowser = chrome

    let browserUrl = "" //"http://testbrowser:4444/wd/hub/" 
    let browser = if browserUrl.Length>0 then Remote(browserUrl, capability) else debugBrowser    
    retry 1000 30 (fun() -> start browser)
  
    SignupTests.all()

    run()

  finally
    quit()

  canopy.runner.classic.failedCount
