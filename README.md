# MapExtension
Run To your castle, avoid the enemies! every level the map grows.

<br/>

## Instructions:

Move with the arrow keys to move the player.

Run from the enemies, if you get hit by them - you will be back to the initial position in the map.

Get to the castle in the map to go to the next level, each level has a bigger map.

<br/>

## Components

Scene Path: **[Assets/Scenes/MapExtension.unity](Assets/Scenes/MapExtension.unity)**

### Changed Scripts:

**[TilemapCaveGenerator](Assets/Scripts/4-generation/TilemapCaveGenerator.cs) -** The main script to use on the Tilemap. Responsible for building the tiles. The changes are documented with comments in the file.
<br />

**[CaveGenerator](Assets/Scripts/4-generation/CaveGenerator.cs) -** Responsible for generating tiles in a matrix form. The changes are documented with comments in the file.
<br />

### New Scripts:

**[PlayerCatcher](Assets/PlayerCatcher.cs) -** Used ny enemies to handle the catching of a player.
<br />

**[WinMap](Assets/WinMap.cs) -** Used by the player to win the game when reaching the winning tile.
<br />

## External Links

Play the game on Itch.io:

https://littlegamers2021.itch.io/mapextension


## **Have Fun!**
