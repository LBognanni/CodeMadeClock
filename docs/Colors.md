# Colors


Clock supports several color styles: solid, linear gradients, conical gradients, and radial gradients

## 🎨 Solid colors

Colors in the serialization format are inspired by HTML and CSS, and can be represented in one of the following formats:

 - `#rrggbbaa`: Each part of the color is encoded by a 2 digits hex value between 00 and FF. The alpha component is optional. For example, fully opaque red is `#ff0000ff` or `#ff0000`. There are a number of online color pickers to help you find the perfect color! [Here is one](https://htmlcolors.com/color-picker)
 - `#rgba` Same as the above but each digit will be automatically doubled. So `#fed3` will be converted to `#ffeedd33`. Again, red will be `#f00f` or `#f00`
 - The name of any [known color](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.knowncolor?view=net-5.0), like `red` or `blue`
 
## ➡ Linear gradients

You can specify linear gradients by using the format `angle-color1-color2[...-colorN]` where `angle` is the angle in degrees, and `color1` and `color2` are the colors in any of the formats above.
Examples of valid linear gradients are:
 - `0-red-blue` will draw a horizontal linear gradient from red to blue
 - `45-red-blue-green` will draw a linear gradient from red to blue to green, angled at 45 degrees
 - `90-#fff-#0008` will draw a vertical linear gradient from solid white to 50% transparent black

## ❇ Radial gradients

Radial gradients take the form of `(cx,cy[,sz])-color1-color2[...-colorN]` where `cx` and `cy` are the coordinates of the center of the gradient, sz is the size of the gradient, and `color1..N` are the colors in any of the formats above.
`cx` and `cy` are expressed in fractional values, where (0.5, 0.5)is the center of the rectangle, and (0, 0) is the top left corner.
`sz` is optional and defaults to 1.

Examples of valid radial gradients are:
 - `(0.5,0.5)-red-green-blue` will draw a radial gradient from red to green to blue, with the center of the gradient at the center of the rectangle, and a radius of half the size of the shape
 - `(0.5,0)-#fff-#000` will draw a radial gradient from white to black, with the center of the gradient at the center of the top edge of the rectangle, and a radius of half the size of the shape
 - `(0.5,0,2)-#fff-#000` will draw a radial gradient from white to black, with the center of the gradient at the center of the top edge of the rectangle, and a radius of the size of the shape

## 🔄 Conical gradients

Conical gradients take the form of `c[-(cx,cy)][-angle]-color1-color2[...-colorN]` where `cx` and `cy` are the coordinates of the center of the gradient, 
`angle` is the starting angle in degrees, and `color1..N` are the colors in any of the formats above.
`cx` and `cy` are optional and default to 0.5, 0.5 (the center of the rectangle). `angle` is also optional and defaults to 0.

Note that the first color is not repeated at the end of the gradient, so if you want a smooth transition you should repeat the first color at the end.

Examples of valid conical gradients are:
 - `c-red-green-blue` will draw a conical gradient from red to green to blue, starting at 0 degrees, and with the center of the gradient at the center of the rectangle
 - `c-red-green-blue-red` will do the same as above, but will have a smooth transition between blue and red
 - `c-(.75,.25)-#fff-#000` will draw a conical gradient from white to black, starting at 0 degrees, and with the center of the gradient at 75% of the width and 25% of the height of the rectangle
 - `c-(.5,.5)-45-#fff-#000` will draw a conical gradient from white to black, starting at 45 degrees, and with the center of the gradient at the center of the rectangle


