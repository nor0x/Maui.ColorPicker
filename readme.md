<img src="https://raw.githubusercontent.com/nor0x/Maui.ColorPicker/main/Art/icon.png" width="200px" />

# Maui.ColorPicker 🎨
a color picker control for .NET MAUI powered on SkiaSharp.

[![.NET](https://github.com/nor0x/Maui.ColorPicker/actions/workflows/dotnet.yml/badge.svg)](https://github.com/nor0x/Maui.ColorPicker/actions/workflows/dotnet.yml)
[![](https://img.shields.io/nuget/v/nor0x.Maui.ColorPicker)](https://www.nuget.org/packages/nor0x.Maui.ColorPicker)
[![](https://img.shields.io/nuget/dt/nor0x.Maui.ColorPicker)](https://www.nuget.org/packages/nor0x.Maui.ColorPicker)


this is largely based on `XFColorPickerControl` for Xamarin.Forms (https://github.com/UdaraAlwis/XFColorPickerControl) by [UdaraAlwis](https://github.com/UdaraAlwis) who allowed me to publish this updated version of the control 🙌

## Getting Started
add namespace
```xml
 xmlns:controls="clr-namespace:Maui.ColorPicker;assembly=Maui.ColorPicker"
```
create control
```xml
<controls:ColorPicker
    x:Name="ColorPicker"
    ColorListDirection="Horizontal"
    GradientColorStyle="DarkToColorsToLightStyle"
    PickedColorChanged="ColorPicker_PickedColorChanged"
    PointerCircleBorderUnits="0.3"
    PointerCircleDiameterUnits="0.7">
</controls:ColorPicker>
```
## Customization
since the control is just a `ContentView` it can be customized in many way - this is an example of a `Clip` applied to the color picker.


https://user-images.githubusercontent.com/3210391/190870180-a7851bc4-c0f1-4b60-8212-b9668aebe091.mp4


