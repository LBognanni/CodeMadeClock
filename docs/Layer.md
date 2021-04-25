# Layer

Represents the base container of Shapes.

Because a Layer is also a Shape, layers can contain other layers.



## Properties
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
    "$type": "Layer",
    "Rotate": 45.2,
    "Offset": {
        "X": 30,
        "Y": 0
    },
    "Shapes": [
        // ... shapes ...
    ]
}

```
