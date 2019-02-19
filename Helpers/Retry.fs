module Retry

open System

let rec retryEx before times fn = 
    if times > 1 then
        try
            fn()
        with 
        | _ -> 
            before()
            retryEx before (times - 1) fn
    else
        fn()        

let retry (delayBefore:int) times fn = retryEx (fun _ -> Threading.Thread.Sleep delayBefore) times fn

