type ActiveCartData = {
    UnpaidItems: string list
}
type PaidCartData = {
    PaidItems: string list;
    Payment: float;
}
// shopping cart state machine
type ShoppingCart =
    | EmptyCart // no data
    | ActiveCart of ActiveCartData
    | PaidCart of PaidCartData

// two event handling functions. Guidelines:
// - (state-changing) event handling functions should always accept and return entire state machine
let addItem cart item =
    match cart with
    | EmptyCart ->
        ActiveCart {UnpaidItems=[item]}
    | ActiveCart {UnpaidItems=existingItems} ->
        ActiveCart {UnpaidItems = item :: existingItems}
    | PaidCart _ ->
        cart
let makePayment cart payment =
    match cart with
    | EmptyCart ->
        cart
    | ActiveCart {UnpaidItems=existingItems} ->
        PaidCart {PaidItems = existingItems; Payment=payment}
    | PaidCart _ ->
        cart