# Canvas

This is the starting block of any rendered image. It contains one or more layers, which in turn contain various shapes
that make up the final image.

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
