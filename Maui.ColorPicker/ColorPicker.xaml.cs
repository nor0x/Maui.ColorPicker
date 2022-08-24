using Microsoft.Maui.Graphics.Converters;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Collections;
using SKPaintSurfaceEventArgs = SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs;

namespace Maui.ColorPicker;

public partial class ColorPicker : ContentView
{
	public ColorPicker()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Occurs when the Picked Color changes
    /// </summary>
    public event EventHandler<Color>? PickedColorChanged;

    public static readonly BindableProperty PickedColorProperty
        = BindableProperty.Create(
            nameof(PickedColor),
            typeof(Color),
            typeof(ColorPicker));

    /// <summary>
    /// Get the current Picked Color
    /// </summary>
    public Color PickedColor
    {
        get { return (Color)GetValue(PickedColorProperty); }
        private set { SetValue(PickedColorProperty, value); }
    }


    public static readonly BindableProperty ColorSpectrumStyleProperty
     = BindableProperty.Create(
         nameof(ColorSpectrumStyle),
         typeof(ColorSpectrumStyle),
         typeof(ColorPicker),
         ColorSpectrumStyle.HueToShadeStyle,
         BindingMode.Default, null,
         propertyChanged: (bindable, value, newValue) =>
         {
             if (newValue != null)
                 ((ColorPicker)bindable).CanvasView.InvalidateSurface();
             else
                 ((ColorPicker)bindable).ColorSpectrumStyle = default;
         });

    /// <summary>
    /// Set the Color Spectrum Gradient Style
    /// </summary>
    public ColorSpectrumStyle ColorSpectrumStyle
    {
        get { return (ColorSpectrumStyle)GetValue(ColorSpectrumStyleProperty); }
        set { SetValue(ColorSpectrumStyleProperty, value); }
    }


    public static readonly BindableProperty BaseColorListProperty
            = BindableProperty.Create(
                nameof(BaseColorList),
                typeof(IEnumerable),
                typeof(ColorPicker),
                new string[]
                {
                    new Color(255, 0, 0).ToHex(), // Red
					new Color(255, 255, 0).ToHex(), // Yellow
					new Color(0, 255, 0).ToHex(), // Green (Lime)
					new Color(0, 255, 255).ToHex(), // Aqua
					new Color(0, 0, 255).ToHex(), // Blue
					new Color(255, 0, 255).ToHex(), // Fuchsia
					new Color(255, 0, 0).ToHex(), // Red
				},
                BindingMode.Default, null,
                propertyChanged: (bindable, value, newValue) =>
                {
                    if (newValue != null)
                        ((ColorPicker)bindable).CanvasView.InvalidateSurface();
                    else
                        ((ColorPicker)bindable).BaseColorList = default;
                });

    /// <summary>
    /// Sets the Base Color List
    /// </summary>
    public IEnumerable BaseColorList
    {
        get { return (IEnumerable)GetValue(BaseColorListProperty); }
        set { SetValue(BaseColorListProperty, value); }
    }


    public static readonly BindableProperty ColorFlowDirectionProperty
        = BindableProperty.Create(
            nameof(ColorFlowDirection),
            typeof(ColorFlowDirection),
            typeof(ColorPicker),
            ColorFlowDirection.Horizontal,
            BindingMode.Default, null,
            propertyChanged: (bindable, value, newValue) =>
            {
                if (newValue != null)
                    ((ColorPicker)bindable).CanvasView.InvalidateSurface();
                else
                    ((ColorPicker)bindable).ColorFlowDirection = default;
            });

    /// <summary>
    /// Sets the Color List flow Direction
    /// Horizontal or Verical
    /// </summary>
    public ColorFlowDirection ColorFlowDirection
    {
        get { return (ColorFlowDirection)GetValue(ColorFlowDirectionProperty); }
        set { SetValue(ColorFlowDirectionProperty, value); }
    }


    public static readonly BindableProperty PointerRingDiameterUnitsProperty
        = BindableProperty.Create(
            nameof(PointerRingDiameterUnits),
            typeof(double),
            typeof(ColorPicker),
            0.6,
            BindingMode.Default,
            validateValue: (bindable, value) =>
            {
                return (((double)value > -1) && ((double)value <= 1));
            },
            propertyChanged: (bindable, value, newValue) =>
            {
                if (newValue != null)
                    ((ColorPicker)bindable).CanvasView.InvalidateSurface();
                else
                    ((ColorPicker)bindable).PointerRingDiameterUnits = default;
            });

    /// <summary>
    /// Sets the Picker Pointer Ring Diameter
    /// Value must be between 0-1
    /// Calculated against the View Canvas size
    /// </summary>
    public double PointerRingDiameterUnits
    {
        get { return (double)GetValue(PointerRingDiameterUnitsProperty); }
        set { SetValue(PointerRingDiameterUnitsProperty, value); }
    }


    public static readonly BindableProperty PointerRingBorderUnitsProperty
        = BindableProperty.Create(
            nameof(PointerRingBorderUnits),
            typeof(double),
            typeof(ColorPicker),
            0.3,
            BindingMode.Default,
            validateValue: (bindable, value) =>
            {
                return (((double)value > -1) && ((double)value <= 1));
            },
            propertyChanged: (bindable, value, newValue) =>
            {
                if (newValue != null)
                    ((ColorPicker)bindable).CanvasView.InvalidateSurface();
                else
                    ((ColorPicker)bindable).PointerRingBorderUnits = default;
            });

    /// <summary>
    /// Sets the Picker Pointer Ring Border Size
    /// Value must be between 0-1
    /// Calculated against pixel size of Picker Pointer
    /// </summary>
    public double PointerRingBorderUnits
    {
        get { return (double)GetValue(PointerRingBorderUnitsProperty); }
        set { SetValue(PointerRingBorderUnitsProperty, value); }
    }


    public static readonly BindableProperty PointerRingPositionXUnitsProperty
        = BindableProperty.Create(
            nameof(PointerRingPositionXUnits),
            typeof(double),
            typeof(ColorPicker),
            0.5,
            BindingMode.OneTime,
            validateValue: (bindable, value) =>
            {
                return (((double)value > -1) && ((double)value <= 1));
            },
            propertyChanged: (bindable, value, newValue) =>
            {
                if (newValue != null)
                {
                    ((ColorPicker)bindable).SetPointerRingPosition(
                        (double)newValue, ((ColorPicker)bindable).PointerRingPositionYUnits);
                }
                else
                    ((ColorPicker)bindable).ColorFlowDirection = default;
            });

    /// <summary>
    /// Sets the Picker Pointer X position
    /// Value must be between 0-1
    /// Calculated against the View Canvas Width value
    /// </summary>
    public double PointerRingPositionXUnits
    {
        get { return (double)GetValue(PointerRingPositionXUnitsProperty); }
        set { SetValue(PointerRingPositionXUnitsProperty, value); }
    }


    public static readonly BindableProperty PointerRingPositionYUnitsProperty
        = BindableProperty.Create(
            nameof(PointerRingPositionYUnits),
            typeof(double),
            typeof(ColorPicker),
            0.5,
            BindingMode.OneTime,
            validateValue: (bindable, value) =>
            {
                return (((double)value > -1) && ((double)value <= 1));
            },
            propertyChanged: (bindable, value, newValue) =>
            {
                if (newValue != null)
                {
                    ((ColorPicker)bindable).SetPointerRingPosition(
                        ((ColorPicker)bindable).PointerRingPositionXUnits, (double)newValue);
                }
                else
                    ((ColorPicker)bindable).ColorFlowDirection = default;
            });

    /// <summary>
    /// Sets the Picker Pointer Y position
    /// Value must be between 0-1
    /// Calculated against the View Canvas Width value
    /// </summary>
    public double PointerRingPositionYUnits
    {
        get { return (double)GetValue(PointerRingPositionYUnitsProperty); }
        set { SetValue(PointerRingPositionYUnitsProperty, value); }
    }


    private SKPoint _lastTouchPoint = new SKPoint();
    private bool _checkPointerInitPositionDone = false;

    private void CanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var skImageInfo = e.Info;
        var skSurface = e.Surface;
        var skCanvas = skSurface.Canvas;

        var skCanvasWidth = skImageInfo.Width;
        var skCanvasHeight = skImageInfo.Height;

        skCanvas.Clear(SKColors.White);

        // Draw gradient rainbow Color spectrum
        using (var paint = new SKPaint())
        {
            paint.IsAntialias = true;

            // Initiate the base Color list
            ColorTypeConverter converter = new ColorTypeConverter();
            System.Collections.Generic.List<SKColor> colors = new System.Collections.Generic.List<SKColor>();
            foreach (var color in BaseColorList)
                colors.Add(((Color)converter.ConvertFromInvariantString(color.ToString())).ToSKColor());

            // create the gradient shader between base Colors
            using (var shader = SKShader.CreateLinearGradient(
                new SKPoint(0, 0),
                ColorFlowDirection == ColorFlowDirection.Horizontal ?
                    new SKPoint(skCanvasWidth, 0) : new SKPoint(0, skCanvasHeight),
                colors.ToArray(),
                null,
                SKShaderTileMode.Clamp))
            {
                paint.Shader = shader;
                skCanvas.DrawPaint(paint);
            }
        }

        // Draw secondary gradient color spectrum
        using (var paint = new SKPaint())
        {
            paint.IsAntialias = true;

            // Initiate gradient color spectrum style layer
            var colors = GetSecondaryLayerColors(ColorSpectrumStyle);

            // create the gradient shader between secondary colors
            using (var shader = SKShader.CreateLinearGradient(
                new SKPoint(0, 0),
                ColorFlowDirection == ColorFlowDirection.Horizontal ?
                    new SKPoint(0, skCanvasHeight) : new SKPoint(skCanvasWidth, 0),
                colors,
                null,
                SKShaderTileMode.Clamp))
            {
                paint.Shader = shader;
                skCanvas.DrawPaint(paint);
            }
        }

        if (!_checkPointerInitPositionDone)
        {
            var x = ((float)skCanvasWidth * (float)PointerRingPositionXUnits);
            var y = ((float)skCanvasHeight * (float)PointerRingPositionYUnits);

            _lastTouchPoint = new SKPoint(x, y);

            _checkPointerInitPositionDone = true;
        }

        // Picking the Pixel Color values on the Touch Point

        // Represent the color of the current Touch point
        SKColor touchPointColor;

        // Efficient and fast
        // https://forums.xamarin.com/discussion/92899/read-a-pixel-info-from-a-canvas
        // create the 1x1 bitmap (auto allocates the pixel buffer)
        using (SKBitmap bitmap = new SKBitmap(skImageInfo))
        {
            // get the pixel buffer for the bitmap
            IntPtr dstpixels = bitmap.GetPixels();

            // read the surface into the bitmap
            skSurface.ReadPixels(skImageInfo,
                dstpixels,
                skImageInfo.RowBytes,
                (int)_lastTouchPoint.X, (int)_lastTouchPoint.Y);

            // access the color
            touchPointColor = bitmap.GetPixel(0, 0);
        }

        // Painting the Touch point
        using (SKPaint paintTouchPoint = new SKPaint())
        {
            paintTouchPoint.Style = SKPaintStyle.Fill;
            paintTouchPoint.Color = SKColors.White;
            paintTouchPoint.IsAntialias = true;

            var canvasLongestLength = (skCanvasWidth > skCanvasHeight)
                    ? skCanvasWidth : skCanvasHeight;

            // Calculate 1/10th of the units value for scaling
            var pointerRingDiameterUnitsScaled = (float)PointerRingDiameterUnits / 10f;
            // Calculate against Longest Length of Canvas 
            var pointerRingDiameter = (float)canvasLongestLength
                                                    * pointerRingDiameterUnitsScaled;

            // Outer circle of the Pointer (Ring)
            skCanvas.DrawCircle(
                _lastTouchPoint.X,
                _lastTouchPoint.Y,
                (pointerRingDiameter / 2), paintTouchPoint);

            // Draw another circle with picked color
            paintTouchPoint.Color = touchPointColor;

            // Calculate against Pointer Circle
            var pointerRingInnerCircleDiameter = (float)pointerRingDiameter
                                                            * (float)PointerRingBorderUnits;

            // Inner circle of the Pointer (Ring)
            skCanvas.DrawCircle(
                _lastTouchPoint.X,
                _lastTouchPoint.Y,
                ((pointerRingDiameter
                        - pointerRingInnerCircleDiameter) / 2), paintTouchPoint);
        }

        // Set selected color
        PickedColor = touchPointColor.ToMauiColor();
        PickedColorChanged?.Invoke(this, PickedColor);
    }

    private void CanvasView_OnTouch(object sender, SKTouchEventArgs e)
    {
#if WINDOWS
        if (!e.InContact)
            return;
#endif

        _lastTouchPoint = e.Location;

        var canvasSize = CanvasView.CanvasSize;

        // Check for each touch point XY position to be inside Canvas
        // Ignore any Touch event occured outside the Canvas region 
        if ((e.Location.X > 0 && e.Location.X < canvasSize.Width) &&
            (e.Location.Y > 0 && e.Location.Y < canvasSize.Height))
        {
            e.Handled = true;

            PointerRingPositionXUnits = e.Location.X / canvasSize.Width;
            PointerRingPositionYUnits = e.Location.Y / canvasSize.Height;

            // update the Canvas as you wish
            CanvasView.InvalidateSurface();
        }
    }

    private SKColor[] GetSecondaryLayerColors(ColorSpectrumStyle colorSpectrumStyle)
    {
        switch (colorSpectrumStyle)
        {
            case ColorSpectrumStyle.HueOnlyStyle:
                return new SKColor[]
                {
                        SKColors.Transparent
                };
            case ColorSpectrumStyle.HueToShadeStyle:
                return new SKColor[]
                {
                        SKColors.Transparent,
                        SKColors.Black
                };
            case ColorSpectrumStyle.ShadeToHueStyle:
                return new SKColor[]
                {
                        SKColors.Black,
                        SKColors.Transparent
                };
            case ColorSpectrumStyle.HueToTintStyle:
                return new SKColor[]
                {
                        SKColors.Transparent,
                        SKColors.White
                };
            case ColorSpectrumStyle.TintToHueStyle:
                return new SKColor[]
                {
                        SKColors.White,
                        SKColors.Transparent
                };
            case ColorSpectrumStyle.TintToHueToShadeStyle:
                return new SKColor[]
                {
                        SKColors.White,
                        SKColors.Transparent,
                        SKColors.Black
                };
            case ColorSpectrumStyle.ShadeToHueToTintStyle:
                return new SKColor[]
                {
                        SKColors.Black,
                        SKColors.Transparent,
                        SKColors.White
                };
            default:
                return new SKColor[]
                {
                        SKColors.Transparent,
                        SKColors.Black
                };
        }
    }

    private void SetPointerRingPosition(double xPositionUnits, double yPositionUnits)
    {
        var xPosition = CanvasView.CanvasSize.Width
                        * xPositionUnits; // Calculate actual X Position
        var yPosition = CanvasView.CanvasSize.Height
                        * yPositionUnits; // Calculate actual Y Position

        // Update as last touch Position on Canvas
        _lastTouchPoint = new SKPoint(Convert.ToSingle(xPosition), Convert.ToSingle(yPosition));
        CanvasView.InvalidateSurface();
    }
}

public enum ColorSpectrumStyle
{
    HueOnlyStyle,
    HueToShadeStyle,
    ShadeToHueStyle,
    HueToTintStyle,
    TintToHueStyle,
    TintToHueToShadeStyle,
    ShadeToHueToTintStyle
}

public enum ColorFlowDirection
{
    Horizontal,
    Vertical
}