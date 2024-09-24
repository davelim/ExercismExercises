module LuciansLusciousLasagna

let expectedMinutesInOven = 40

let remainingMinutesInOven minutesInOven =
    expectedMinutesInOven - minutesInOven

let preparationTimeInMinutes numLayers =
    numLayers * 2

let elapsedTimeInMinutes numLayers minutesInOven =
    preparationTimeInMinutes numLayers + minutesInOven