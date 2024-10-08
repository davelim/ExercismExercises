module AsyncProgramming

open System

let userTimerWithCallback =
    // create an event to wait on
    let event = new System.Threading.AutoResetEvent(false)

    // create a timer and add an event handler that will signal the "event"
    let timer = new System.Timers.Timer(2000.0)
    timer.Elapsed.Add (fun _ ->
        printfn "timer elapsed, \"signal\" event"
        event.Set() |> ignore
    )

    // start timer
    printfn "Waiting for timer at %O" DateTime.Now.TimeOfDay
    timer.Start()

    // keep working
    printfn "Doing something useful while waiting for event"
    printfn "working...1"
    System.Threading.Thread.Sleep(1000);
    printfn "working...2"
    // System.Threading.Thread.Sleep(1000);
    printfn "working...3"
    // System.Threading.Thread.Sleep(1000);
    printfn "working...4"

    // block on the timer via the AutoResetEvent
    event.WaitOne() |> ignore

    //done
    printfn "Timer ticked at %O" DateTime.Now.TimeOfDay


let userTimerWithAsync =
    // create a timer and associated async event
    let timer = new System.Timers.Timer(2000.0)
    // asynchronous workflow - object to encapsulate a background task
    let timerEvent = Async.AwaitEvent (timer.Elapsed) |> Async.Ignore

    // start
    printfn "Waiting for timer at %O" DateTime.Now.TimeOfDay
    timer.Start()

    // keep working
    printfn "Doing something useful while waiting for event"

    // block on the timer event now by waiting for the async to complete
    Async.RunSynchronously timerEvent

    // done
    printfn "Timer ticked at %O" DateTime.Now.TimeOfDay