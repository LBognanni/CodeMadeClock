# RotateRepeatLayer

A layer whose contents will get drawn rotated at various angles.

It's especially useful for repeating hour and minute ticks on a clock face.

For example, you could have a rectangle at (-1, -20) with size(2, 10), when setting 
the value of RepeatCount to 12 and the value of RepeatRotate to 30 you will have drawn the hour ticks on a clock.
This is because the rectangle will be drawn 12 times, each time rotated by 30 degrees more than the previous.



## Properties
### RepeatCount

Number of times the content will be repeated



### RepeatRotate

The angle(deg) at which to repeat the contents



### Start

The angle(deg) at which to start repeating the contents 



### Shapes

A list of all the shapes contained in this layer



### Rotate

Layer rotation, in degrees



### Offset

Layer offset in Canvas Units



See [Canvas](Canvas.md)

---

## Example

```json
{
    "$type": "RotateRepeatLayer",
    "Shapes": [
        {
            "$type": "Shape",
            "Color": "#aefe",
            "Path": "0,-42 1.5,-40 0,-35 -1.5,-40"
        }
    ],
    "Offset": {
        "X": 49,
        "Y": 49
    },
    "RepeatRotate": 30,
    "RepeatCount": 12
}

```
