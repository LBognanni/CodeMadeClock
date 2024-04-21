# RectangleShape

A rectangle



## Properties
### Left

X coordinate of the leftmost side



### Top

Y coordinate of the top side



### Width

Width of the rectangle



### Height

Height of the rectangle



### Color

Fill color



See [Colors](Colors.md)
### Smooth

True to enable anti-aliasing on this rectangle. Set this to `true` if the rectangle is going to be rotated. 



### CornerRadius

The radius of the corners of the rectangle. If set to 0, the rectangle will have sharp corners.
If set to a value greater than 0, the rectangle will have rounded corners, and the "Smooth" option will also implicitly be true.




---

## Example

```json
{
    "$type": "RectangleShape",
    "Left": -1.5,
    "Top": -60,
    "Width": 3,
    "Height": 11,
    "Color": "#0008"
}

```
