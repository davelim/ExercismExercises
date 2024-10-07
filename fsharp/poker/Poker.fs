module Poker

type CardRank = 
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
type HandType = // Note: order matters
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
let sortCardRanks (cardArr: string seq): CardRank seq =
    cardArr
    |> Seq.map getCardRank
    |> Seq.sortDescending
let getCnt (tupl: CardRank * CardRank seq): int =
    tupl
    |> snd
    |> Seq.length
let groupCardRanks (cardRankSeq: CardRank seq): (CardRank * CardRank seq) list =
    cardRankSeq
    |> Seq.groupBy id
    |> Seq.sortByDescending getCnt
    |> Seq.toList

let allCardsHaveTheSameSuit (cardArr: string array): bool =
    let sameSuit (cardX: string) (cardY:string) =
        (cardX |> getCardSuit) = (cardY |> getCardSuit)
    let firstCard = cardArr |> Seq.head
    cardArr |> Seq.forall (sameSuit firstCard)
let cardsMonotonicByOne (cardRanks: CardRank seq): bool =
    let monotoneByOne (twoCards: CardRank * CardRank) = 
        let (a, b) = twoCards
        int b - int a = 1
    cardRanks
    |> Seq.sort
    |> Seq.pairwise
    |> Seq.forall monotoneByOne
let cardsLowestStraight (cardRanks: CardRank seq): bool =
    (cardRanks |> Seq.contains CardRank.Ace)
    && (cardRanks |> Seq.contains CardRank.Five)
    && (cardRanks |> Seq.contains CardRank.Four)
    && (cardRanks |> Seq.contains CardRank.Three)
    && (cardRanks |> Seq.contains CardRank.Two)

let getHandType (cardRankCount: int list) (flush: bool) (straight: bool): HandType =
    match cardRankCount with
    | [4;1] -> FourOfAKind
    | [3;2] -> FullHouse
    | [3;1;1] -> ThreeOfAKind
    | [2;2;1] -> TwoPair
    | [2;1;1;1] -> OnePair
    | _ ->
        if straight && flush then
            StraightFlush
        elif straight then
            Straight
        elif flush then
            Flush
        else
            HighCard

let mapHand (hand: string): Hand =
    let cardArr = hand |> getCardArr
    let sortedCards = cardArr |> sortCardRanks
    let groupedCards = sortedCards |> groupCardRanks
    let scoringCards =
        groupedCards
        |> List.map (fun t -> t |> fst)

    let isFlush = cardArr |> allCardsHaveTheSameSuit
    let lowestStraight = sortedCards |> cardsLowestStraight
    let regularStraight = sortedCards |> cardsMonotonicByOne
    let isStraight = lowestStraight || regularStraight

    let cardRankCount = groupedCards |> List.map getCnt
    let handType = getHandType cardRankCount isFlush isStraight
    match handType with
    | FourOfAKind | FullHouse | ThreeOfAKind | TwoPair | OnePair | Flush | HighCard ->
        Hand(hand, handType, scoringCards)
    | _ (*Straight | StraightFlush*) ->
        let scoringCards =
            if lowestStraight then [CardRank.Five] else [sortedCards |> Seq.head]
        Hand(hand, handType, scoringCards)

let bestHands (hands: string list): string list =
    let sortedHands =
        hands
        |> Seq.map mapHand
        |> Seq.sortDescending
    let bestHand = sortedHands |> Seq.head
    sortedHands
    |> Seq.filter (fun h -> h = bestHand)
    |> Seq.map (fun h -> h.Str)
    |> Seq.toList