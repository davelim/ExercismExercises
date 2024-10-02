// Compare to EmailContactInfo in DesignWithTypes...
// type EmailContactInfo = {
//     EmailAddress: EmailAddress.T; // union type wrapped primitive
//     IsEmailVerified: bool;
// }
// Instead of boolean flags e.g. IsEmailVerified, 
// use explicity states, e.g. UnverifiedData/VerifiedData
module EmailContactInfo =
    open System

    type EmailAddress = string  //placeholder
    type UnverifiedData = EmailAddress
    type VerifiedData = EmailAddress * DateTime

    type T =
        | UnverifiedState of UnverifiedData
        | VerifiedState of VerifiedData

    // ctor
    let create email =
        UnverifiedState email
    
    // handle "verified" event
    let verified emailContactInfo dateVerified =
        match emailContactInfo with
        | UnverifiedState email ->
            VerifiedState (email, dateVerified)
        | VerifiedState _ ->
            emailContactInfo

    // utility functions
    let sendVerificationEmail emailContactInfo =
        match emailContactInfo with
        | UnverifiedState email -> printfn "sending email"
        | VerifiedState _ -> ()
    let sendPasswordReset emailContactInfo =
        match emailContactInfo with
        | UnverifiedState email -> ()
        | VerifiedState _ -> printfn "sending password reset"

// see: https://fsharpforfunandprofit.com/posts/designing-with-types-representing-states/
// 1. Replace case/switch with explicit types/cases
// - instead of
//   - Package with PackageStatus (enum) field, and 
//   - if PackageStatus.undelivered else if PackageStatus.outForDelivery ...
// - different types for package
//   - Package type Undelivered, OutForDelivery, ...

// 2. Replace implicit conditional with explicit types/cases
// - instead of a record type Order with optional fields,
//   e.g. PaidDate, ShippedDate
// - make Order a discriminated union type for data,
//   e.g. PaidOrderData, ShippedOrderData

// Conditions for using simple state machines
// - set of mutually exclusive states with transitions between them
// - transitions are triggered by external events
// - states are exhaustive
// - each state might have associated data that shold not be accessible when
//   the system is in another state
// - there are static business rules that apply to the states