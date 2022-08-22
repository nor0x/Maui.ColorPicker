<img src="https://raw.githubusercontent.com/nor0x/Maui.ColorPicker/main/art/icon.png" width="200px" />

# Maui.ColorPicker 🎨
a color picker control for .NET MAUI powered on SkiaSharp.

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