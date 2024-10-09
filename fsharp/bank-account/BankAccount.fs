module BankAccount

type AccountType =
    | NewAccount
    | OpenedAccount
    | ClosedAccount
type Account() =
    // https://fsharpforfunandprofit.com/posts/concurrency-actor-model/
    // - The locking approach to shared state
    let _lock = System.Object()
    let mutable AType = AccountType.NewAccount
    let mutable Balance = 0.0m

    member this.GetType = AType
    member this.GetBalance = Balance
    member this.SetType t =
        AType <- t
    member this.UpdateBalance c =
        lock _lock (fun () ->
            Balance <- this.GetBalance + c
        )

let mkBankAccount() = new Account()

let openAccount (account: Account) =
    // if account is already opened, or closed, nothing to do
    if (account.GetType = AccountType.NewAccount) then
        account.SetType AccountType.OpenedAccount
    account

let closeAccount (account: Account) =
    // if account is new, or already closed, nothing to do
    if (account.GetType = AccountType.OpenedAccount) then
       account.SetType AccountType.ClosedAccount
    account

let getBalance (account: Account) =
    // if account is new, or closed, no balance to speak of
    if (account.GetType = AccountType.OpenedAccount) then
        Some account.GetBalance
    else
        None

let updateBalance change (account: Account) =
    if (account.GetType = AccountType.OpenedAccount) then
        account.UpdateBalance change
    account