//define a "safe" email address type
type EmailAddress = EmailAddress of string
//define a function that uses it
let sendEmail (EmailAddress email) =
   printfn "sent an email to %s" email
//try to send one
let aliceEmail = EmailAddress "alice@example.com"
sendEmail aliceEmail
//sendEmail "bob@example.com"   //error: sendEmail requires "EmailAddress" type

// units of measurement:
// - define units of measure
// - associate them with floats
// - unit of measure is then "attached" to float as a type
[<Measure>]
type cm
[<Measure>]
type inches
[<Measure>]
type feet =
   // add a conversion function
   static member toInches(feet : float<feet>) : float<inches> =
      feet * 12.0<inches/feet>
// define some values
let meter = 100.0<cm>
let yard = 3.0<feet>
//convert to different measure
let yardInInches = feet.toInches(yard)
//yard + meter  // error: can't mix and match!
// now define some currencies
[<Measure>]
type GBP
[<Measure>]
type USD
let gbp10 = 10.0<GBP>
let usd10 = 10.0<USD>
gbp10 + gbp10             // allowed: same currency
// gbp10 + usd10          // error: different currency
// gbp10 + 1.0            // error: didn't specify a currency
gbp10 + 1.0<_>            // allowed using wildcard

open System
let obj = new Object()
let ex = new Exception()
let b = (obj = ex)

// deny comparison
[<NoEquality; NoComparison>]
type CustomerAccount = {CustomerAccountId: int}
let x = {CustomerAccountId = 1}
// x = x       // error!
x.CustomerAccountId = x.CustomerAccountId // no error
