// record type
type PersonalName = {
    FirstName: string;
    MiddleInitial: string option;
    LastName: string;
}
type EmailContactInfo = {
    EmailAddress: string;
    IsEmailVerified: bool;
}
type PostalAddress = {
    Address1: string;
    Address2: string;
    City: string;
    State: string;
    Zip: string;
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
