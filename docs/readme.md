# ⏰ Authoring new skins

Clock's skins are made by vector shapes stored in `json` format. 
You can create

You can start Clock in Preview Mode by passing the `-p` command line parameter and the path to the skin file you're working on:

```
CodeMade.Clock.exe -p [path to the json file]
```

## 💫 Basic concepts
A clock skin is compose hierarchically by one [Canvas](Canvas.md) that contains one or more [layers](Layer.md).

Layers can contain other layers or [shapes](Shape.md).

When defining elements in json, each object will contain a `"$type"` property that distinguishes what kind of shape or layer it is.

### 🥞 Types of Layers 
There are several types of layers:

 - [The basic layer](Layer.md), is a transparent container of other layers and shapes. It can define a transformation in terms of position and rotation that will be applied to all its children.
 - The [SolidLayer](SolidLayer.md) is essentially the same as the normal Layer but defines a background color.
 - The [GaussianBlurLayer](GaussianBlurLayer.md), which will blur anything that it contains.
 - The [RotateRepeatLayer](RotateRepeatLayer.md), which will draw anything it contains multiple times at various angles.
 - The [NumbersLayer](NumbersLayer.md) is useful for drawing the numbers on a clock's face.

Plus of course there are layers that will change with time:

 - [HoursLayer](HoursLayer.md) will rotate according to the hour
 - [MinutesLayer](MinutesLayer.md) will rotate according to the minute
 - [SecondsLayer](SecondsLayer.md) will rotate according to the second

### ⚜ Types of shapes 
Everyting that gets drawn by Clock is composed by arranging various types of shapes:

 - 🔴 [CircleShape](CircleShape.md)
 - 🟦 [RectangleShape](RectangleShape.md)
 - 🗯 a generic polygon [Shape](Shape.md) that is composed by a set of points
 - 🖼 a [BitmapShape](BitmapShape) that loads and displays an image
 - 🔠 [TextShape](TextShape.md) is used to draw text

### 🌈 Color theory

Shapes can be filled with solid colors, linear and conical gradients. [See how](Colors.md)