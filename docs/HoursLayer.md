# HoursLayer

A layer that rotates every hour



## Properties
### Angle

[Optional] The angle this layer will rotate by at the hour interval



### Is24Hours

Only used if `Angle` is not specified.
If true, the layer will complete a full rotation in 24 hours, otherwise it will do so in 12 hours.
Default value is `false`



### TimeZone

The time zone for this layer.
Can be any of the [valid time zone names in tzdb](https://en.wikipedia.org/wiki/List_of_tz_database_time_zones)



### Smooth

If set at the default value of `false`, this layer will rotate once at every interval.
If set to `true`, the layer's rotation will be updated more frequently



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
    "$type": "HoursLayer",
    "Shapes": [
     // ... shapes ...
    ]
},

```
