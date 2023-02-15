# Kinglet.Color

[![.NET](https://github.com/ebeeton/Kinglet.Color/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ebeeton/Kinglet.Color/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/ebeeton/Kinglet.Color/actions/workflows/codeql.yml/badge.svg)](https://github.com/ebeeton/Kinglet.Color/actions/workflows/codeql.yml)

This library currently has a single use case: define a gradient that can be used
to generate an array of colors - a palette - of an arbitrary size.

A gradient is made up of stops. Each stop has a position in [0,1] and a color.

This gradient has the minimum of two stops, with black on one end and white on
the other.

```C#
var gradient = new Gradient
{
    Stops = new List<GradientStop>
    {
        new GradientStop(0, new Rgba32(0, 0, 0, 255)),
        new GradientStop(1, new Rgba32(255, 255, 255, 255)),
    }
};
```

If we wanted a palette of 10 colors (or shades of gray in this case), we could
do so with the following code.

```C#
var palette = gradient.GetPalette(10);
```

We could then print each element of the palette to standard output.

```C#
foreach (var color in palette)
{
    Console.WriteLine(color);
}

// Standard Output: 
// R: 0 G: 0 B: 0 A: 255
// R: 28 G: 28 B: 28 A: 255
// R: 56 G: 56 B: 56 A: 255
// R: 85 G: 85 B: 85 A: 255
// R: 113 G: 113 B: 113 A: 255
// R: 141 G: 141 B: 141 A: 255
// R: 170 G: 170 B: 170 A: 255
// R: 198 G: 198 B: 198 A: 255
// R: 226 G: 226 B: 226 A: 255
// R: 255 G: 255 B: 255 A: 255
```


