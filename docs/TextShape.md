# TextShape

Draws a string of text at the specified coordinates.
The text will be either printed using a system font specified in `Font` or a specific .ttf font file specified in `FontFile`



## Properties
### Text

Text to print



### FontName

Name of the font. It should be a font that is installed in the system.

If `FontFile` is specified, this will be ignored.



### Color

Color of the text. It should be a simple color, gradients are not supported.



See [Colors](Colors.md)
### Centered

`true` if the text should be centered at `Position`, false if it shoud begin at `Position`



### Font

Alias of FontName



### FontSize

Font size



### FontFile

Name of a custom font file that should be redistributed with this skin.
If using this, the `Font` property is ignored




---

## Example

```json
{
    "$type": "TextShape",
    "Text": "codemade.net",
    "Font": "Tahoma",
    "FontSize": 6,
    "Color": "#cefc",
    "Centered": true,
    "Position": {
        "X": 50,
        "Y": 67
    }
}

```
