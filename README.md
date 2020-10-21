# Helm's Deep GIF Maker -> (Simple Test Project )

![Helms Deep](SalesPitch.PNG)

This is a short C# project designed to replicate a hand-drawn micro .GIF (animated) image ,by automating the rules I followed while hand-drawing the GIF.
![AnimatedGIF](HelmsDeep.gif)


## Notes/Know Bugs:

> No known bugs at the moment. Please email bugfinder@starshiplad.com with any bugs you find.

## Features(Planned In Brackets)

> Automates the hand-drawing process used to draw https://twitter.com/StarshipladDevp/status/1300752966299734017

> Animated .GIF file

> Export .GIF file after creation

> Animates the inital frame as a PNG


## View of progress

![Progress Image](Progress.png)
*The below GIF file is output by the program*
![Progress Gif] (Progressgif.gif)

## Latest Build
Still In progress

## Latest Update

21/10/2020 -Comments & Background City:

WorkerFunctions ->Add Comments to all major Functions

Globals -> Add a 'backgroundCityHeight' Array that acts similar to Globals.cityHeight and wallHeight to match handdrawn GIFs

WorkerFunctions FillCells -> Add a 'backgroundCityHeight' generator that fills cells similar to Globals.cityHeight and wallHeight to match handdrawn GIFs

SiegeFunction CellTypes -> Add 'BackgroundCity' Celltype to draw handdraw GIF backgroundCity pixels

WorkerFunctions DispalyCells -> As per above changes, add handling of drawing 'backgroundCity' tiles to match handdrawn GIFs

WorkerFunctions DispalyCells ->  Add parameter 'frame', add Title Change of refrenced form to dispaly the current 'frame' being drawn, so that progress can be tracked

## Next Build

Week ending 11/10/2020 -Release 1.0 ***DELAYED*** New Release 31/10/2020

* Add casulty/unit removal after enough 'wounds'

* Improve the Background City, maybe bomb handling

* GIF Output

* Help button for .GIF example


## Skill developing

I plan on this project improving my skills in the following:

> C#

> File exporting in .NET framework

>Pixel Art and Animation

## Installing and Compiling:
* NOTE * Windows machine only

1) Un-ZIP the 'Executable' ZIP file after downloading this repo
2) Double click the 'HelmsDeep.exe' file in the extracted folder
3) Hit 'Start Helm's Deep Siege' To begin animating in the window
4) The Finished .GIF file will be stored in the dir you ran the program from.
![HelpImage](Help.PNG)
