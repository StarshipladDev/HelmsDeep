# Helm's Deep GIF Maker -> (Simple Test Project )

![Helms Deep](SalesPitch.PNG)

This is a short C# project designed to replicate a handdrawn micro .GIF animated image by
automating the rules I followed while hand drawing the GIF.
![AnimatedGIF](HelmsDeep.gif)


## Notes/Know Bugs:

> 'Night' Tiles may go down so far there is no 'City' tiles between 'Top Wall' Tiles and 'Night'

## Features(Planned In Brackets)

> (Aniamted .GIF file)

> (Export .GIF file of project)

> Automates the hand-drawing process used to draw https://twitter.com/StarshipladDevp/status/1300752966299734017

> Animates the inital frame


## View of progress

![Progress Image](Progress.png)

## Latest Build
Still In progress

## Latest Update

15/09/2019 - Unit Handling
	WorkerFunctions.cs -> Refractor 'FillCells' component that set what cell would be what type to a 'setCell' funtion.
	WorkerFunctions.cs -> Modify 'RunAnimation' so it spawns new units at the end and completes new ladders from last cycle, for aesthetic.
	WorkerFunctions.cs -> Modify 'SetCell' so it spawns new units only 4 tiles away from the bottom of the wall, for aesthetic.
	WorkerFunctions.cs -> Modify 'DisplayCells' so that after drawing, it sprays grey lines on 'Urkahai' & 'Rohan' tiles. This is for aesthetic.
		This required passing a Random instance to 'DislayCells'
	WorkerFunctions.cs -> Modify 'SetCell' so wall top fills up or down depending on leftmost walltop, to bulk up walls
	WorkerFunctions.cs -> Modify 'SetupSiegeAnimation' so there is a 33% change Rohan or Urkahai units touching will be 'wounded'.
		This method now also resets wounded on each loop, so a unit will only be wounded for 1 cycle. This required making
		'SetupSiegeAnimation' take a 'Random' instacne.
	SiegeFunctions.cs -> Modify 'SiegeCell' to have a 'wounded' bool. WorkerFunctions.cs 'DispalyCell' will draw as red if true.
	WorkerFunctions.cs -> Refractor 'RunAnimations' component that made it so area below the unit that has moved on will be reset, 
		regardless of direction rather than only on 'UP' movements. Added handling for 'LEFT' and 'RIGHT' movements
	WorkerFunctions.cs -> Modify 'SetupSiegeAnimation' so 'Urkahai' or 'UrkahaiElites' will move left or right if on the wall.
		This is for aesthetics

## Next Build

Week ending 11/10/2020 -Release 1.0

* Add casulty/unit removal after enough 'wounds'

* Improve the Background City, add bomb handling

* GIF Output

* Help button for .GIF example


## Skill developing

I plan on this project improving my skills in the following:

> C#

> File exporting in .NET framework

>Pixel Art and Animation

## Installing and Compiling:
* NOTE * Windows machine only

1) UnZIP the 'Executable' ZIP file after downloading this repo
2) Double click the 'HelmsDeep.exe' file in the extracted folder
3) Hit 'Start Helm's Deep Siege' To begin animating in the window
![HelpImage](Help.PNG)
