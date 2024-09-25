module PizzaPricing

type Pizza =
    | Margherita
    | Caprese
    | Formaggio
    | ExtraSauce of Pizza
    | ExtraToppings of Pizza

let rec pizzaPrice (pizza: Pizza): int =
    match pizza with
    | Margherita -> 7
    | Caprese -> 9
    | Formaggio -> 10
    | ExtraSauce pizza -> 1 + pizzaPrice pizza
    | ExtraToppings pizza -> 2 + pizzaPrice pizza

let orderPrice(pizzas: Pizza list): int = failwith "Please implement the 'orderPrice' function"
