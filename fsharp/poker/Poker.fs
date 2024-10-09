module Poker

let (|ValidSuit|_|) suit =
    match suit with
    | 'C' | 'D' | 'H' | 'S' -> Some suit
    | _ -> None
let getCardSuit (card: string): char =
    match (card |> Seq.last) with
    | ValidSuit s -> s
    | _ -> invalidArg "card" $"'{card}' has an invalid suit"

let (|ValidRank|_|) rank =
    match rank with
    | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" | "10" | "J" | "Q" | "K" | "A" ->
        Some rank
    | _ ->
        None
type CardRank = CardRank of int
let getCardRank (card: string): CardRank =
    let suit = card |> getCardSuit
    match card.TrimEnd(suit) with
    | ValidRank rank ->
        try
            // "2" through "10"
            rank |> int |> CardRank
        with :? System.FormatException ->
            // "J" through "A"
            match rank with
            | "J" -> CardRank 11
            | "Q" -> CardRank 12
            | "K" -> CardRank 13
            | _ (*"A"*) -> CardRank 14
    | _ -> invalidArg "card" $"'{card}' has an invalid rank"

type HandType = // Note: order matters, see Hand.CompareTo()
    | HighCard
    | OnePair
    | TwoPair
    | ThreeOfAKind
    | Straight
    | Flush
    | FullHouse
    | FourOfAKind
    | StraightFlush
type Hand (str: string, handType: HandType, cardRanks: CardRank list) =
    member this.Str = str
    member this.HandType = handType
    member this.CardRanks = cardRanks

    interface System.IComparable with
        member this.CompareTo (other) =
            match other with
            | :? Hand as h ->
                if this.HandType = h.HandType then
                    compare this.CardRanks h.CardRanks
                else
                    compare this.HandType h.HandType
            | _ -> invalidArg "other" "cannot compare values of different types"
    override this.Equals (other) =
        match other with
        | :? Hand as h -> this.HandType = h.HandType && this.CardRanks = h.CardRanks
        | _ -> invalidArg "other" "cannot compare values of different types"
    override this.GetHashCode () = hash (this.Str, this.HandType, this.CardRanks)

let getCardArr (hand: string): string array = hand.Split(" ")
let sortCardRanks (cardArr: string array): CardRank list =
    cardArr
    |> Seq.map getCardRank
    |> Seq.sortDescending
    |> Seq.toList
let getCnt (tupl: CardRank * CardRank list): int =
    tupl
    |> snd
    |> List.length
let groupCardRanks (cardRankList: CardRank list): (CardRank * CardRank list) list =
    cardRankList
    |> List.groupBy id
    |> List.sortByDescending getCnt

let allCardsHaveTheSameSuit (cardArr: string array): bool =
    let sameSuit (cardX: string) (cardY:string) =
        (cardX |> getCardSuit) = (cardY |> getCardSuit)
    let firstCard = cardArr |> Seq.head
    cardArr |> Seq.forall (sameSuit firstCard)
let cardsMonotonicByOne (cardRanks: CardRank list): bool =
    let monotoneByOne (twoCards: CardRank * CardRank) = 
        let (CardRank a, CardRank b) = twoCards
        b - a = 1
    cardRanks
    |> List.sort
    |> List.pairwise
    |> List.forall monotoneByOne
let cardsLowestStraight (cardRanks: CardRank list): bool =
    // "A", "5", "4", "3", "2"
    cardRanks = [CardRank 14;CardRank 5;CardRank 4;CardRank 3;CardRank 2]

let (|GroupedCardsHandTypes|_|) groupedCards =
    let cardRankToCnt = groupedCards |> List.map getCnt
    match cardRankToCnt with
    | [4;1] -> Some FourOfAKind
    | [3;2] -> Some FullHouse
    | [3;1;1] -> Some ThreeOfAKind
    | [2;2;1] -> Some TwoPair
    | [2;1;1;1] -> Some OnePair
    | _ -> None
let mapHand (hand: string): Hand =
    let cardArr = hand |> getCardArr
    let sortedCards = cardArr |> sortCardRanks
    let groupedCards = sortedCards |> groupCardRanks

    let lowestStraight = sortedCards |> cardsLowestStraight
    let regularStraight = sortedCards |> cardsMonotonicByOne

    let scoringCards =
        if lowestStraight then
            [CardRank 5]
        elif regularStraight then
            [sortedCards |> List.head]
        else
            groupedCards
            |> List.map (fun t -> t |> fst)
    let handType =
        match groupedCards with
        // pair, three of kind, full house, etc.
        | GroupedCardsHandTypes htype -> htype
        | _ ->
            // flush, straight, high card
            let isFlush = cardArr |> allCardsHaveTheSameSuit
            let isStraight = lowestStraight || regularStraight
            if isStraight && isFlush then
                StraightFlush
            elif isStraight then
                Straight
            elif isFlush then
                Flush
            else
                HighCard

    Hand(hand, handType, scoringCards)

let bestHands (hands: string list): string list =
    let allHands = hands |> List.map mapHand
    let bestHand = allHands |> List.max
    allHands
    |> List.filter(fun h -> h = bestHand)
    |> List.map (fun h -> h.Str)