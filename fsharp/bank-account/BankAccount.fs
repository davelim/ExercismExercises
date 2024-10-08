module BankAccount

type AccountData = { Balance: decimal }
type Account =
    | NewAccount
    | OpenedAccount of AccountData
    | ClosedAccount
// TODO: keep balance around for ClosedAccount? Is Closed-to-Opened ok?

let mkBankAccount() = NewAccount

let openAccount account =
    match account with
    | NewAccount ->
        OpenedAccount {Balance = 0.0m}
    | OpenedAccount _ ->
        account
    | ClosedAccount ->
        account

let closeAccount account =
    match account with
    | NewAccount ->
        account
    | OpenedAccount _ ->
        ClosedAccount
    | ClosedAccount ->
        account

let getBalance account =
    match account with
    | NewAccount ->
        None
    | OpenedAccount {Balance=balance} ->
        Some balance
    | ClosedAccount ->
        None

let updateBalance change account =
    match account with
    | NewAccount ->
        account
    | OpenedAccount {Balance=balance} ->
        OpenedAccount {Balance = balance + change}
    | ClosedAccount ->
        account