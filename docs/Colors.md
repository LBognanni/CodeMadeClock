# Colors

## 🎨 Solid colors

Colors in the serialization format are inspired by HTML and CSS, and can be represented in one of the following formats:

 - `#rrggbbaa`: Each part of the color is encoded by a 2 digits hex value between 00 and FF. The alpha component is optional. For example, fully opaque red is `#ff0000ff` or `#ff0000`. There are a number of online color pickers to help you find the perfect color! [Here is one](https://htmlcolors.com/color-picker)
 - `#rgba` Same as the above but each digit will be automatically doubled. So `#fed3` will be converted to `#ffeedd33`. Again, red will be `#f00f` or `#f00`
 - The name of any [known color](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.knowncolor?view=net-5.0), like `red` or `blue`
 
## Linear gradients

It's also possible to specify linear gradients by using the format `angle-color1-color2` where `angle` is the angle in degrees, and `color1` and `color2` are the colors in any of the formats above.
For example a 30 degrees gray gradient could be `30-#ccc-gray`


