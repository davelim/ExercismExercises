module Poker
// https://fsharpforfunandprofit.com/posts/convenience-types/
// https://fsharpforfunandprofit.com/series/designing-with-types/
// TODO: implement this module
// 1. type for Suit (club, diamond, heart, spade)
// TODO: handle invalid suit? Use option?
type Suit = Suit of char

// 2. type for Rank (2, 3, ..., A)
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
// TODO: handle invalid rank? Use option?
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

// 3. type for Card - record with Rank and Suit
type Card (rank: CardRank, suit: Suit) =
    member this.Rank = rank
    member this.Suit = suit
    // Copied from Expert F#
    // https://learning.oreilly.com/library/view/expert-f-4-0/9781484207406/9781484207413_Ch09.xhtml#Sec30
    interface System.IComparable with
        member this.CompareTo (other) =
            match other with
            | :? Card as c -> compare this.Rank c.Rank
            | _ -> invalidArg "other" "cannot compare values of different types"
    override this.Equals (other) =
        match other with
        | :? Card as c -> this.Rank = c.Rank
        | _ -> invalidArg "other" "cannot compare values of different types"
    override this.GetHashCode () = hash this.Rank

let createCard (card: string): Card =
    let (rank, suit) =
        match card |> String.length with
        | 3 -> (card[0..1], card[2])
        | _ -> (card[0].ToString(),  card[1])
    new Card ((rank |> toCardRank), (suit |> Suit))

// 4. types of Hands (ranked)
type StraightFlushData = {
    HighRank: CardRank;
}
type FourOfAKindData = {
    FourCardRank: CardRank;
    FifthCardRank: CardRank;
}
type FullHouseData = {
    ThreeCardRank: CardRank;
    PairRank: CardRank;
}
type FlushData = {
    CardRanks: CardRank seq;
}
type StraightData = {
    HighRank: CardRank;
}
type ThreeOfAKindData = {
    ThreeCardRank: CardRank;
    OtherRanks: CardRank seq;
}
type TwoPairData = {
    HighPairRank: CardRank;
    LowPairRank: CardRank;
    FifthCardRank: CardRank;
}
type PairData = {
    PairRank: CardRank;
    OtherCardRanks: CardRank seq;
}
type HighCardData = {
    CardRanks: CardRank seq;
}

type Hand =
| HighCard of HighCardData
| OnePair of PairData
| TwoPair of TwoPairData
| ThreeOfAKind of ThreeOfAKindData
| Straight of StraightData
| Flush of FlushData
| FullHouse of FullHouseData
| FourOfAKind of FourOfAKindData
| StraightFlush of StraightFlushData

let getCardSuit (card: string): char = card |> Seq.last
let getCardRank (card: string): CardRank =
    let suit = card |> getCardSuit
    card.TrimEnd(suit) |> toCardRank

let getCardArr (hand: string): string array = hand.Split(" ")
let sortCardRanks (cardArr: string seq): CardRank seq =
    cardArr
    |> Seq.map getCardRank
    |> Seq.sort
let getCnt (tupl: CardRank * CardRank seq): int =
    tupl |> snd |> Seq.length
let groupCardRanks (cardRankSeq: CardRank seq): (CardRank * CardRank seq) list =
    cardRankSeq
    |> Seq.groupBy id
    |> Seq.sortByDescending getCnt
    |> Seq.toList

// Flush
let flush = "AH 6H KH 4H 10H"
let notFlush = "AH 6H KH 4H 10C"
let allCardsHaveTheSameSuit (cardArr: string array): bool =
    let sameSuit (cardX: string) (cardY:string) =
        (cardX |> getCardSuit) = (cardY |> getCardSuit)
    let firstCard = cardArr |> Seq.head
    cardArr |> Seq.forall (sameSuit firstCard)

// Straight
let lowStraight = "4S AH 3S 2D 5H"
let regularStraight = "4H 8S 6D 5C 7S"
let notStraight = "4H 8S 6D 5C AS"
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

// HighCard/OnePair/TwoPair/ThreeOfAKind/FullHouse/FourOfAKind
let rankHand (hand: string): Hand =
    let cardArr = hand |> getCardArr
    let sortedCardRanks = cardArr |> sortCardRanks
    let groupedCardRankList = sortedCardRanks |> groupCardRanks

    let isFlush = cardArr |> allCardsHaveTheSameSuit
    let isLowestStraight = sortedCardRanks |> cardsLowestStraight
    let isStraight = sortedCardRanks |> cardsMonotonicByOne

    let getRank (tupl: CardRank * CardRank seq): CardRank =
        tupl |> fst
    match (groupedCardRankList |> List.map getCnt) with
    | [4;1] ->
        let [fourCards;fifthCard] = groupedCardRankList
        FourOfAKind {
            FourCardRank = fourCards |> getRank;
            FifthCardRank = fifthCard |> getRank;
        }
    | [3;2] ->
        let [threeCards;pair] = groupedCardRankList
        FullHouse {
            ThreeCardRank = threeCards |> getRank;
            PairRank = pair |> getRank;
        }
    | [3;1;1] ->
        let [threeCards;high;low] = groupedCardRankList
        ThreeOfAKind {
            ThreeCardRank = threeCards |> getRank;
            OtherRanks = seq {
                high |> getRank;
                low |> getRank};
        }
    | [2;2;1] ->
        let [highPair;lowPair;fifthCard] = groupedCardRankList
        TwoPair {
            HighPairRank = highPair |> getRank;
            LowPairRank = lowPair |> getRank;
            FifthCardRank = fifthCard |> getRank;
        }
    | [2;1;1;1] ->
        let [pair;highCard;midCard;lowCard] = groupedCardRankList
        OnePair {
            PairRank = pair |> getRank;
            OtherCardRanks = seq {
                highCard |> getRank;
                midCard |> getRank;
                lowCard |> getRank;
            }
        }
    | _ ->
        let highCardRank = sortedCardRanks |> Seq.head
        if isFlush then
            if isLowestStraight then
                StraightFlush { HighRank = CardRank.Five }
            elif isStraight then
                StraightFlush { HighRank = highCardRank }
            else
                Flush { CardRanks = sortedCardRanks }
        else
            if isLowestStraight then
                Straight { HighRank = CardRank.Five }
            elif isStraight then
                Straight { HighRank = highCardRank }
            else
                HighCard { CardRanks = sortedCardRanks }
    
let bestHands (hands: string list): string list =
    // failwith "Not implemented."
    (*
    hands
    |> Seq.map rankHand
    |> Seq.max/Seq.maxBy/Seq.sort
    |> Seq.head
    *)
