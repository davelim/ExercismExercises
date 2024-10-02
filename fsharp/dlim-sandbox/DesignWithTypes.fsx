// wrap primitive types
#load "EmailAddress.fs"
open EmailAddress

// - usually using single union type (easier to wrap/unwrap)
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
    EmailAddress: EmailAddress.T; // union type wrapped primitive
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

type ContactInfo =
    | EmailOnly of EmailContactInfo
    | PostOnly of PostalContactInfo
    | EmailAndPost of EmailContactInfo * PostalContactInfo

type Contact = {
    Name: PersonalName;
    ContactInfo: ContactInfo;
}