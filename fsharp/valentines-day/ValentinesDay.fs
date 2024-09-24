module ValentinesDay

type Approval = | Yes | No | Maybe

type Cuisine = | Korean | Turkish

type Genre = | Crime | Horror | Romance | Thriller

type Activity =
    | BoardGame
    | Chill
    | Movie of Genre
    | Restaurant of Cuisine
    | Walk of int

let rateActivity (activity: Activity): Approval = failwith "Please implement the 'rateActivity' function"
