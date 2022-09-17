<img src="https://raw.githubusercontent.com/nor0x/Maui.ColorPicker/main/Art/icon.png" width="200px" />

# Maui.ColorPicker 🎨
a color picker control for .NET MAUI powered by SkiaSharp.

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


## Bindable Properties

### ```ColorPicked```: 
Gets the Picked Color of the Color Picker.

XAML:
```xml
<controls:ColorPicker
    x:Name="ColorPicker"
    ColorPicked={Binding UserPickedColor}
    ... >
</controls:ColorPicker>
```
C#:
```csharp
var colorPicked = ColorPicker.ColorPicked;
```

### ```BaseColorList```: 

<img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/GIFS/BaseColorList.gif" height="180"/> 

Change the available base Colors on the Color Spectrum, of the Color Picker. This will take a **List of strings included with Color names or hex values** which is held in an IEnumerable.
#### XML:
```xml
<controls:ColorPicker
    x:Name="ColorPicker"
    ... >
    <controls:ColorPicker.BaseColorList>
        <x:Array Type="{x:Type x:String}">
            <!--  Yellow  -->
            <x:String>#ffff00</x:String>
            <!--  Aqua  -->
            <x:String>#00ffff</x:String>
            <!--  Fuchsia  -->
            <x:String>#ff00ff</x:String>
            <!--  Yellow  -->
            <x:String>#ffff00</x:String>
        </x:Array>
    </controls:ColorPicker.BaseColorList>
</controls:ColorPicker>
```
#### C#:
```csharp
ColorPicker.BaseColorList = new List<string>()
{
    "#00bfff",
    "#0040ff",
    "#8000ff",
    "#ff00ff",
    "#ff0000",
};
```

### ```ColorFlowDirection```: 
Change the direction in which the Colors are flowing through on the Color Spectrum, of the Color Picker. This will allow you to set whether the Colors are flowing through from left to right, **Horizontally** or top to bottom, **Vertically**

#### XAML
```xml
<controls:ColorPicker
    x:Name="ColorPicker"
    ColorFlowDirection="Horizontal"
    ... >
</controls:ColorPicker>
```
#### C#:
```csharp
ColorPicker.ColorFlowDirection =
    Udara.Plugin.controls.ColorFlowDirection.Horizontal;
```

### ```ColorSpectrumStyle```: 

<img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/GIFS/ColorSpectrumStyle.gif" height="180"/> 

Change the Color Spectrum gradient style, with the rendering combination of base colors (**Hue**), or lighter colors (**Tint**) or darker colors (**Shade**).

Available Styles: 
- HueOnlyStyle
- HueToShadeStyle
- ShadeToHueStyle
- HueToTintStyle
- TintToHueStyle
- TintToHueToShadeStyle
- ShadeToHueToTintStyle

#### XML:
```xml
<controls:ColorPicker
    x:Name="ColorPicker"
    ColorSpectrumStyle="TintToHueToShadeStyle"
    ... >
</controls:ColorPicker>
```
#### C#:
```csharp
ColorPicker.ColorSpectrumStyle = 
    Udara.Plugin.controls.ColorSpectrumStyle.TintToHueToShadeStyle;
```

### ```PointerRingDiameterUnits```:

<img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/GIFS/PointerRingSize.gif" height="180"/> 

Changes the Diameter size of the Pointer Ring on the Color Picker. It accepts values between 0 and 1, as a representation of numerical units which is compared to the 1/10th of the longest length of the Color Picker Canvas. By default this value is set to 0.6 units.

#### XML:
```xml
<controls:ColorPicker
    x:Name="ColorPicker"
    PointerRingDiameterUnits="0.6"
    ...    >
</controls:ColorPicker>
```
#### C#:
```csharp
ColorPicker.PointerRingDiameterUnits = 0.6;
```

### ```PointerRingBorderUnits```:

<img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/GIFS/PointerRingSize.gif" height="180"/> 

Changes the Border Thickness size of the Pointer Ring on the Color Picker. It accepts values between 0 and 1, as a representation of numerical units which is calculated against the diameter of the Pointer Ring. By default this value is set to 0.3 units.

#### XML:
```xml
<controls:ColorPicker
    x:Name="ColorPicker"
    PointerRingBorderUnits="0.3"
    ...    >
</controls:ColorPicker>
```
#### C#:
```csharp
ColorPicker.PointerRingBorderUnits = 0.3;
```

### ```PointerRingPosition<X,Y>Units```:

<img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/GIFS/PointerRingPosition.gif" height="180"/> 

Changes the Pointer Ring’s position on the Color Picker Canvas programmatically. There are of two bindable properties **PointerRingPositionXUnits** and **PointerRingPositionYUnits**, which represents X and Y coordinates on the Color Picker Canvas. It accepts values between 0 and 1, as a presentation of numerical units which is calculated against the Color Picker Canvas’s actual pixel Width and Height. By default both the values are set to 0.5 units, which positions the Pointer Ring in the center of the Color Picker.

#### XML:
```xml
<controls:ColorPicker
    x:Name="ColorPicker"
    PointerRingPositionXUnits="0.3"
    PointerRingPositionYUnits="0.7"
    ...    >
</controls:ColorPicker>
```
#### C#:
```csharp
ColorPicker.PointerRingPositionXUnits = 0.3;
ColorPicker.PointerRingPositionYUnits = 0.7;
```

## Event Handler

Fires every time when the selected Color is changed

### ```PickedColorChanged```: 
Gets the pickedColor, object type of Color 

```xml
<controls:ColorPickerControl
        x:Name="ColorPicker"
        PickedColorChanged="ColorPicker_PickedColorChanged" />
```

#### C#:
```csharp
ColorPicker.PickedColorChanged += ColorPicker_PickedColorChanged;
```
```csharp
private void ColorPicker_PickedColorChanged(object sender, Color colorPicked)
{
    //Do whatever you want with the colorPicker 
}
```


## Customization
since the control is just a `ContentView` it can be customized in many way - this is an example of a `Clip` applied to the color picker.


https://user-images.githubusercontent.com/3210391/190870180-a7851bc4-c0f1-4b60-8212-b9668aebe091.mp4





<details>
  <summary>⬇️ Xamarin.Forms Version README.md ⬇️</summary>


Color Picker Control for Xamarin.Forms!
===========

Interactive and responsive Color Picker Control for Xamarin.Forms (Android, iOS, UWP) with a whole bunch of awesome features. On a Canvas with a beautiful Color spectrum similar to a rainbow gradient effect spreading across, drag, drop, swipe and pan over the Canvas to pick the Color you need easily, in a fun-to-use interactive experience. Built from pure Xamarin.Forms based on SkiaSharp, lightweight and fast!

<img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/XFColorPickerControl.png" height="300"/> <img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/Demo iOS.png" height="300"/> 

Setting it up:

* Grab it on NuGet: https://www.nuget.org/packages/Udara.Plugin.XFColorPickerControl/ [![NuGet](https://img.shields.io/nuget/v/Udara.Plugin.XFColorPickerControl.svg?label=NuGet)](https://www.nuget.org/packages/Udara.Plugin.XFColorPickerControl/)
* Just install in your Xamarin.Forms PCL/.NET Standard  project, you're good to go!

Blog post:
https://theconfuzedsourcecode.wordpress.com/2020/03/17/publishing-the-nuget-of-my-color-picker-control-for-xamarin-forms/


## XAML Set up

```xml
xmlns:xfsegmentedcontrol="clr-namespace:Udara.Plugin.XFColorPickerControl;assembly=Udara.Plugin.XFColorPickerControl"
```

```xml
<xfColorPickerControl:ColorPicker
	x:Name="ColorPicker"
	ColorFlowDirection="Horizontal"
	ColorSpectrumStyle="TintToHueToShadeStyle"
	HeightRequest="200"
	HorizontalOptions="FillAndExpand"
	PickedColorChanged="ColorPicker_PickedColorChanged"
	PointerRingBorderUnits="0.3"
	PointerRingDiameterUnits="0.7"
	PointerRingPositionXUnits="0.6"
	PointerRingPositionYUnits="0.6">
	<xfColorPickerControl:ColorPicker.BaseColorList>
		<x:Array Type="{x:Type x:String}">
			<!--  Red  -->
			<x:String>#ff0000</x:String>
			<!--  Yellow  -->
			<x:String>#ffff00</x:String>
			<!--  Green (Lime)  -->
			<x:String>#00ff00</x:String>
			<!--  Aqua  -->
			<x:String>#00ffff</x:String>
			<!--  Blue  -->
			<x:String>#0000ff</x:String>
			<!--  Fuchsia  -->
			<x:String>#ff00ff</x:String>
			<!--  Red  -->
			<x:String>#ff0000</x:String>
		</x:Array>
	</xfColorPickerControl:ColorPicker.BaseColorList>
</xfColorPickerControl:ColorPicker>
```

## PickedColorChanged Event

```csharp
private void ColorPicker_PickedColorChanged(object sender, Color colorPicked)
{
        // do whatever you want with the colorPicked value
}
```

## Bindable Properties

### ```ColorPicked```: 
Gets the Picked Color of the Color Picker.

XAML:
```xml
<xfColorPickerControl:ColorPicker
    x:Name="ColorPicker"
    ColorPicked={Binding UserPickedColor}
    ... >
</xfColorPickerControl:ColorPicker>
```
C#:
```csharp
var colorPicked = ColorPicker.ColorPicked;
```

### ```BaseColorList```: 

<img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/GIFS/BaseColorList.gif" height="180"/> 

Change the available base Colors on the Color Spectrum, of the Color Picker. This will take a **List of strings included with Color names or hex values** which is held in an IEnumerable.
#### XML:
```xml
<xfColorPickerControl:ColorPicker
    x:Name="ColorPicker"
    ... >
    <xfColorPickerControl:ColorPicker.BaseColorList>
        <x:Array Type="{x:Type x:String}">
            <!--  Yellow  -->
            <x:String>#ffff00</x:String>
            <!--  Aqua  -->
            <x:String>#00ffff</x:String>
            <!--  Fuchsia  -->
            <x:String>#ff00ff</x:String>
            <!--  Yellow  -->
            <x:String>#ffff00</x:String>
        </x:Array>
    </xfColorPickerControl:ColorPicker.BaseColorList>
</xfColorPickerControl:ColorPicker>
```
#### C#:
```csharp
ColorPicker.BaseColorList = new List<string>()
{
    "#00bfff",
    "#0040ff",
    "#8000ff",
    "#ff00ff",
    "#ff0000",
};
```

### ```ColorFlowDirection```: 
Change the direction in which the Colors are flowing through on the Color Spectrum, of the Color Picker. This will allow you to set whether the Colors are flowing through from left to right, **Horizontally** or top to bottom, **Vertically**

#### XAML
```xml
<xfColorPickerControl:ColorPicker
    x:Name="ColorPicker"
    ColorFlowDirection="Horizontal"
    ... >
</xfColorPickerControl:ColorPicker>
```
#### C#:
```csharp
ColorPicker.ColorFlowDirection =
    Udara.Plugin.XFColorPickerControl.ColorFlowDirection.Horizontal;
```

### ```ColorSpectrumStyle```: 

<img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/GIFS/ColorSpectrumStyle.gif" height="180"/> 

Change the Color Spectrum gradient style, with the rendering combination of base colors (**Hue**), or lighter colors (**Tint**) or darker colors (**Shade**).

Available Styles: 
- HueOnlyStyle
- HueToShadeStyle
- ShadeToHueStyle
- HueToTintStyle
- TintToHueStyle
- TintToHueToShadeStyle
- ShadeToHueToTintStyle

#### XML:
```xml
<xfColorPickerControl:ColorPicker
    x:Name="ColorPicker"
    ColorSpectrumStyle="TintToHueToShadeStyle"
    ... >
</xfColorPickerControl:ColorPicker>
```
#### C#:
```csharp
ColorPicker.ColorSpectrumStyle = 
    Udara.Plugin.XFColorPickerControl.ColorSpectrumStyle.TintToHueToShadeStyle;
```

### ```PointerRingDiameterUnits```:

<img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/GIFS/PointerRingSize.gif" height="180"/> 

Changes the Diameter size of the Pointer Ring on the Color Picker. It accepts values between 0 and 1, as a representation of numerical units which is compared to the 1/10th of the longest length of the Color Picker Canvas. By default this value is set to 0.6 units.

#### XML:
```xml
<xfColorPickerControl:ColorPicker
    x:Name="ColorPicker"
    PointerRingDiameterUnits="0.6"
    ...    >
</xfColorPickerControl:ColorPicker>
```
#### C#:
```csharp
ColorPicker.PointerRingDiameterUnits = 0.6;
```

### ```PointerRingBorderUnits```:

<img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/GIFS/PointerRingSize.gif" height="180"/> 

Changes the Border Thickness size of the Pointer Ring on the Color Picker. It accepts values between 0 and 1, as a representation of numerical units which is calculated against the diameter of the Pointer Ring. By default this value is set to 0.3 units.

#### XML:
```xml
<xfColorPickerControl:ColorPicker
    x:Name="ColorPicker"
    PointerRingBorderUnits="0.3"
    ...    >
</xfColorPickerControl:ColorPicker>
```
#### C#:
```csharp
ColorPicker.PointerRingBorderUnits = 0.3;
```

### ```PointerRingPosition<X,Y>Units```:

<img src="https://github.com/UdaraAlwis/XFColorPickerControl/raw/master/screenshots/GIFS/PointerRingPosition.gif" height="180"/> 

Changes the Pointer Ring’s position on the Color Picker Canvas programmatically. There are of two bindable properties **PointerRingPositionXUnits** and **PointerRingPositionYUnits**, which represents X and Y coordinates on the Color Picker Canvas. It accepts values between 0 and 1, as a presentation of numerical units which is calculated against the Color Picker Canvas’s actual pixel Width and Height. By default both the values are set to 0.5 units, which positions the Pointer Ring in the center of the Color Picker.

#### XML:
```xml
<xfColorPickerControl:ColorPicker
    x:Name="ColorPicker"
    PointerRingPositionXUnits="0.3"
    PointerRingPositionYUnits="0.7"
    ...    >
</xfColorPickerControl:ColorPicker>
```
#### C#:
```csharp
ColorPicker.PointerRingPositionXUnits = 0.3;
ColorPicker.PointerRingPositionYUnits = 0.7;
```

## Event Handler

Fires every time when the selected Color is changed

### ```PickedColorChanged```: 
Gets the pickedColor, object type of Color 

```xml
<controls:ColorPickerControl
        x:Name="ColorPicker"
        PickedColorChanged="ColorPicker_PickedColorChanged" />
```

#### C#:
```csharp
ColorPicker.PickedColorChanged += ColorPicker_PickedColorChanged;
```
```csharp
private void ColorPicker_PickedColorChanged(object sender, Color colorPicked)
{
    //Do whatever you want with the colorPicker 
}
```


</details>

