// wrap primitive types
// - usually using single union type (easier to wrap/unwrap)
type EmailAddress = EmailAddress of string
let CreateEmailAddress (s:string) =
    if System.Text.RegularExpressions.Regex.IsMatch(s, @"^\S+@\S+\.\S+$")
        then s |> EmailAddress |> Some
        else None
type ZipCode = ZipCode of string
type StateCode = StateCode of string
let CreateStateCode (s:string) =
    let s' = s.ToUpper()
    let stateCodes = ["AZ";"CA";"NY"] //etc
    if stateCodes |> List.exists ((=) s')
        then Some (StateCode s')
        else None

// record types
type PersonalName = {
    FirstName: string;
    MiddleInitial: string option;
    LastName: string;
}
type EmailContactInfo = {
    EmailAddress: EmailAddress; // union type wrapped primitive
    IsEmailVerified: bool;
}
type PostalAddress = {
    Address1: string;
    Address2: string;
    City: string;
    State: StateCode;
    Zip: ZipCode;
}
type PostalContactInfo = {
    Address: PostalAddress;
    IsAddressValid: bool;
}

type Contact = {
    Name: PersonalName;
    EmailContactInfo: EmailContactInfo;
    PostalContactInfo: PostalContactInfo;
}
