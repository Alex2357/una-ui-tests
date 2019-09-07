namespace Tests


open NUnit.Framework
open VCanopy.NUnit
open System
open OpenQA.Selenium.Chrome
open OpenQA.Selenium
open OpenQA.Selenium.Remote
open System

[<SetUpFixture>]
type TestsSetup () =

    let createChromeDriver():IWebDriver = 
        let driver = new ChromeDriver("/usr/bin")
        driver.Manage().Window.Size <- System.Drawing.Size(1050,1000)        
        printfn "BrowserSize %A" (driver.Manage().Window.Size)
        driver.Manage().Timeouts().AsynchronousJavaScript <- TimeSpan.FromMinutes(1.0)
        upcast driver

    let createRemoteDriver():IWebDriver =
        let browserUrl = "" //"http://testbrowser:4444/wd/hub/" 
        let browserName = "chrome"

        let capability = OpenQA.Selenium.Remote.DesiredCapabilities()
        capability.SetCapability("browserName", browserName) 

        let driver = new RemoteWebDriver(Uri(browserUrl), capability, TimeSpan.FromMinutes(3.0))
        printfn "BrowserSize %A" (driver.Manage().Window.Size)

        upcast driver

    [<OneTimeSetUp>]    
    member this.GlobalSetup () = 
        setDriverFactory createChromeDriver
        setConfig {WebDriverInstanceCount = 4; CompleteDriverRelease = true}

    [<OneTimeTearDown>]   
    member this.GlobalTeardown () = ()