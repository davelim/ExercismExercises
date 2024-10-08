module Poker

type CardRank = // int needed for straight, see cardsMonotonicByOne
    | Two = 2
    | Three = 3
    | Four = 4
    | Five = 5
    | Six = 6
    | Seven = 7
    | Eight = 8
    | Nine = 9
    | Ten = 10
    | Jack = 11
    | Queen = 12
    | King = 13
    | Ace = 14
// TODO: handle invalid rank?
let toCardRank (rank: string): CardRank =
    match rank with
    | "2" -> CardRank.Two
    | "3" -> CardRank.Three
    | "4" -> CardRank.Four
    | "5" -> CardRank.Five
    | "6" -> CardRank.Six
    | "7" -> CardRank.Seven
    | "8" -> CardRank.Eight
    | "9" -> CardRank.Nine
    | "10" -> CardRank.Ten
    | "J" -> CardRank.Jack
    | "Q" -> CardRank.Queen
    | "K" -> CardRank.King
    | _ (*"A"*) -> CardRank.Ace
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

// TODO: handle invalid suit?
let getCardSuit (card: string): char = card |> Seq.last
let getCardRank (card: string): CardRank =
    let suit = card |> getCardSuit
    card.TrimEnd(suit) |> toCardRank

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
        let (a, b) = twoCards
        int b - int a = 1
    cardRanks
    |> List.sort
    |> List.pairwise
    |> List.forall monotoneByOne
let cardsLowestStraight (cardRanks: CardRank list): bool =
    cardRanks = [
        CardRank.Ace;
        CardRank.Five;
        CardRank.Four;
        CardRank.Three;
        CardRank.Two
    ]

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

    let isFlush = cardArr |> allCardsHaveTheSameSuit
    let lowestStraight = sortedCards |> cardsLowestStraight
    let regularStraight = sortedCards |> cardsMonotonicByOne
    let isStraight = lowestStraight || regularStraight

    let scoringCards =
        if lowestStraight then
            [CardRank.Five]
        elif regularStraight then
            [sortedCards |> List.head]
        else
            groupedCards
            |> List.map (fun t -> t |> fst)
    match groupedCards with
    | GroupedCardsHandTypes htype ->  // e.g. Four of a kind, pair, etc.
        Hand(hand, htype, scoringCards)
    | _ ->
        if isStraight && isFlush then
            Hand(hand, StraightFlush, scoringCards)
        elif isStraight then
            Hand(hand, Straight, scoringCards)
        elif isFlush then
            Hand(hand, Flush, scoringCards)
        else
            Hand(hand, HighCard, scoringCards)

let bestHands (hands: string list): string list =
    let sortedHands =
        hands
        |> List.map mapHand
        |> List.sortDescending
    let bestHand = sortedHands |> List.head
    sortedHands
    |> List.filter (fun h -> h = bestHand)
    |> List.map (fun h -> h.Str)