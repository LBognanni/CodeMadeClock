# NumbersLayer

Use this layer to draw the hours numbers on your clock



## Properties
### Numbers

[Optional] An array of numbers that will be drawn. Default value is all hours (`[1,2,3,4,5,6,7,8,9,10,11,12]`)



### NumbersText

[Optional] An array of strings that corresponds to the text shown for each hour defined in `Numbers`.
Default value is the same as `Numbers`



### FontName

Name of the font to be used to render `NumbersText`



### FontSize

Size of the font



### Color

Text color. Cannot be a gradient.



See [Colors](Colors.md)
### Radius

The radius 
```
          12
         /|    1
   radius |     
        \ |       2
         \|
9---------+---------3
          |
          |
          |
          |
          6

```



### RotateMode

[Optional] How the text is rotated. Can be one of the following values:
 - `0` (default) - No rotation
 - `1` - Text is rotated so that it faces inside the center
 - `2` - Text is rotated so that it faces outside the center



### Is24Hours

`true` if you want to have a 24-hour dial



### FontFile

[Optional] Alternative to `FontName`, you can specify a custom .ttf font file



See [TextShape](TextShape.md)

---

## Example

```json
{
	"$type": "NumbersLayer",
	"Numbers": [
		3,
		6,
		9,
		12
	],
	"NumbersText": [
	    "III",
	    "VI",
	    "IX",
	    "XII"
	],
	"Color": "#cedf",
	"FontName": "Times New Roman",
	"FontSize": 16,
	"Radius": 35,
	"Offset": {
		"X": 52,
		"Y": 52.5
	}
}

```
