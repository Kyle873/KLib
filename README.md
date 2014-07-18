KLIB
====

A 2D XNA Game Engine I was working on to use with some game ideas I had. Basic features that were more or less working include

* Actor System
* Basic Console with the beginnings of a command system
* Simple input handling/checking for Mouse and Keyboard
* Rendering of Sprites, Shapes and Text
* Applying shaders to Actors
* Beginnings of a Particle System
* Dynamic Popoffs System
* Camera and Cursor system, able to handle picking from screen space to world space
* Tweening System
* Timing System
* Basic Sound playback and layered music system using FMOD Ex's horrible .NET bindings
* Basic GUI, including a base control class and basic controls including Bars, Buttons, Icons, Labels and Windows to encapsulate them
* Tooltips
* BMFont handling via the BMFont AngelCode BMFont XML Serializer
* Tweening and scaling functions handling by TinyTween by Nick Gravelyn
* Originally I also started working on a TileEd renderer/parser, but didn't end up needing it for any of my ideas so I never finished it

I ultimately stopped working on the project due to the death of XNA and the fact that porting everything, how basic it is, to MonoGame became an absolute chore for the easiest things, such as font handling and having no usable content pipeline and requiring all assets to be run through the XNA pipeline, further slowing the process down

One day I might reuse the code or update it to use a future C# graphics engine or reincarnation of XNA, if that ever actually happens
