# Canvas

This is the starting block of any rendered image. It contains one or more layers, which in turn contain various shapes
that make up the final image.

Most importantly, it defines the Width and Height properties, which represent the Canvas Units that are used everywhere else. 
Canvas Units don't necessarily match with pixels because, being skins can be rendered at any size.

There is no need to specify $type because Canvas is only to be used as the root element.



## Properties
### Width

Suggested output width.
Should be an integer value greater than zero.



### Height

Suggested output height
Should be an integer value greater than zero.



### Layers

A collection of layers that make up the image



See [Layer](Layer.md), [SolidLayer](SolidLayer.md), [GaussianBlurLayer](GaussianBlurLayer.md), [RotateRepeatLayer](RotateRepeatLayer.md)

---

## Example

```json
{
    "Width": 101,
    "Height": 101,
    "Layers": [
       // ... layers ...
    ]
}

```
