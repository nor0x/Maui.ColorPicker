using Microsoft.Maui.Graphics.Converters;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Collections;
using SKPaintSurfaceEventArgs = SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs;

namespace Maui.ColorPicker;
/// <summary>
/// A control that allows the user to pick a <see cref="Color"/>.
/// </summary>
public partial class ColorPicker : ContentView
{
    public ColorPicker()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Checks whether this <see cref="ColorPicker"/> is in a rendering state.
    /// A <see cref="ColorPicker"/> in a rendering state cannot have its properties
    /// modified by outside code.
    /// </summary>
    private bool _rendering = false;
    private Color? _pendingPickedColor = null;
    private SKColor _touchPointColor;

    /// <summary>
    /// Occurs when the Picked Color changes
    /// </summary>
    public event EventHandler<PickedColorChangedEventArgs>? PickedColorChanged;

    public static readonly BindableProperty PickedColorProperty
        = BindableProperty.Create(
            nameof(PickedColor),
            typeof(Color),
            typeof(ColorPicker),
            propertyChanged: (bindable, value, newValue) =>
            {
                if (newValue is null) return;
                if (!newValue.Equals(value) && (bindable is ColorPicker picker))
                {
                    picker.PickedColorChanged?
                        .Invoke(picker, new PickedColorChangedEventArgs((Color?)value, (Color)newValue));
                    if (!picker._rendering)
                    {
                        picker._pendingPickedColor = (Color)newValue;
                        picker.CanvasView.InvalidateSurface();
                    }
                }
            });

    /// <summary>
    /// Gets and sets the current picked <see cref="Color"/>. This is a bindable property.
    /// </summary>
    /// <value>
    /// A <see cref="Color"/> containing the picked color. The default value is <see langword="null"/>.
    /// </value>
    /// <remarks>
    /// Setting this value to <see langword="null"/> makes the control honor the values set
    /// to <see cref="PointerRingPositionXUnits"/> and <see cref="PointerRingPositionYUnits"/>
    /// instead.
    /// <br/>
    /// Setting this property will cause the <see cref="PickedColorChanged"/> event to be emitted.
    /// </remarks>
    public Color PickedColor
    {
        get { return (Color)GetValue(PickedColorProperty); }
        set
        {
            if (!_rendering)
            {
                SetValue(PickedColorProperty, value);
            }
        }
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
    /// Gets or sets the Color Spectrum Gradient Style.
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
                if ((double)newValue != (double)value && bindable is ColorPicker picker && !picker._rendering)
                {
                    picker._pendingPickedColor = null;
                    picker.CanvasView.InvalidateSurface();
                }
            });

    /// <summary>
    /// Sets the Picker Pointer X position
    /// Value must be between 0-1
    /// Calculated against the View Canvas Width value
    /// </summary>
    public double PointerRingPositionXUnits
    {
        get { return (double)GetValue(PointerRingPositionXUnitsProperty); }
        set
        {
            if (!_rendering)
            {
                SetValue(PointerRingPositionXUnitsProperty, value);
            }
        }
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
                if ((double)newValue != (double)value && bindable is ColorPicker picker && !picker._rendering)
                {
                    picker._pendingPickedColor = null;
                    picker.CanvasView.InvalidateSurface();
                }
            });

    /// <summary>
    /// Gets or sets the type of touch action that triggers the color selection.
    /// </summary>
    /// <value>
    /// The type of touch action. The default is <see cref="TouchActionType.OnTouchDown"/>.
    /// </value>
    public TouchActionType TouchActionType
    {
        get => (TouchActionType)GetValue(TouchActionTypeProperty);
        set => SetValue(TouchActionTypeProperty, value);
    }

    public static readonly BindableProperty BitmapProperty =
    BindableProperty.Create(
        nameof(Bitmap),
        typeof(SKBitmap),
        typeof(ColorPicker),
        null);

    /// <summary>
    /// Gets and Sets the background bitmap.
    /// </summary>
    /// <value>
    /// A bitmap to use instead of the colors/>.
    /// </value>
    public SKBitmap Bitmap
    {
        get => (SKBitmap)GetValue(BitmapProperty);
        set => SetValue(BitmapProperty, value);
    }

    public static readonly BindableProperty TouchActionTypeProperty =
    BindableProperty.Create(
        nameof(TouchActionType),
        typeof(TouchActionType),
        typeof(ColorPicker),
        TouchActionType.OnTouchDown);

    /// <summary>
    /// Sets the Picker Pointer Y position
    /// Value must be between 0-1
    /// Calculated against the View Canvas Width value
    /// </summary>
    public double PointerRingPositionYUnits
    {
        get { return (double)GetValue(PointerRingPositionYUnitsProperty); }
        set
        {
            if (!_rendering)
            {
                SetValue(PointerRingPositionYUnitsProperty, value);
            }
        }
    }


    private void CanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        _rendering = true;

        var skImageInfo = e.Info;
        var skSurface = e.Surface;
        var skCanvas = skSurface.Canvas;

        var skCanvasWidth = skImageInfo.Width;
        var skCanvasHeight = skImageInfo.Height;

        skCanvas.Clear(SKColors.White);

        if (Bitmap != null)
        {
            // Draw the bitmap
            var bitmapRect = new SKRect(0, 0, skCanvasWidth, skCanvasHeight);
            skCanvas.DrawBitmap(Bitmap, bitmapRect);
        }
        else
        {
            // Draw gradient rainbow Color spectrum
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;

                // Initiate the base Color list
                ColorTypeConverter converter = new ColorTypeConverter();
                var colors = BaseColorList
                    .Cast<object>()
                    .Select(color => converter.ConvertFromInvariantString(color?.ToString() ?? string.Empty))
                    .Where(color => color != null)
                    .Cast<Color>()
                    .Select(color => color.ToSKColor())
                    .ToList();

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
        }

        SKPoint touchPoint;
        // Represent the color of the current Touch point

        if (_pendingPickedColor == null)
        {
            // The user hasn't explicitly specified the touchPoint color.
            // The touchPoint can therefore be calculated quickly.
            touchPoint = new SKPoint(
                x: skCanvasWidth * (float)PointerRingPositionXUnits,
                y: skCanvasHeight * (float)PointerRingPositionYUnits);
            // Picking the Pixel Color values on the Touch Point

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
                    (int)touchPoint.X, (int)touchPoint.Y);

                // access the color
                _touchPointColor = bitmap.GetPixel(0, 0);
            }

            // Set selected color on touch down or prepare the pending color for touch up
            if (TouchActionType == TouchActionType.OnTouchUp)
                _pendingPickedColor = _touchPointColor.ToMauiColor();
            else
                SetValue(PickedColorProperty, _touchPointColor.ToMauiColor());
        }
        else
        {
            // We'll have to brute force the board to find the nearest color.
            _touchPointColor = _pendingPickedColor.ToSKColor();
            using var bitmap = new SKBitmap(skImageInfo);
            var dstpixels = bitmap.GetPixels();
            skSurface.ReadPixels(skImageInfo, dstpixels, skImageInfo.RowBytes, 0, 0);

            int desiredX = -1;
            int desiredY = -1;
            int nearestDesiredX = -1;
            int nearestDesiredY = -1;
            int distance = int.MaxValue;

            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    var currentColor = bitmap.GetPixel(x, y);
                    if (currentColor == _touchPointColor)
                    {
                        desiredX = x;
                        desiredY = y;
                        goto found;
                    }
                    else
                    {
                        var currentDistance =
                        Math.Abs(currentColor.Red - _touchPointColor.Red) +
                        Math.Abs(currentColor.Green - _touchPointColor.Green) +
                        Math.Abs(currentColor.Blue - _touchPointColor.Blue) +
                        Math.Abs(currentColor.Alpha - _touchPointColor.Alpha);

                        if (currentDistance < distance)
                        {
                            distance = currentDistance;
                            nearestDesiredX = x;
                            nearestDesiredY = y;
                        }
                    }
                }
            }
        found:
            if (desiredX != -1 && desiredY != -1)
            {
                touchPoint = new SKPoint(desiredX, desiredY);
            }
            else
            {
                touchPoint = new SKPoint(nearestDesiredX, nearestDesiredY);
            }

            // Set pointer position.
            SetValue(PointerRingPositionXUnitsProperty, (double)touchPoint.X / skCanvasWidth);
            SetValue(PointerRingPositionYUnitsProperty, (double)touchPoint.Y / skCanvasHeight);
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
                touchPoint.X,
                touchPoint.Y,
                (pointerRingDiameter / 2), paintTouchPoint);

            // Draw another circle with picked color
            paintTouchPoint.Color = _touchPointColor;

            // Calculate against Pointer Circle
            var pointerRingInnerCircleDiameter = (float)pointerRingDiameter
                                                            * (float)PointerRingBorderUnits;

            // Inner circle of the Pointer (Ring)
            skCanvas.DrawCircle(
                touchPoint.X,
                touchPoint.Y,
                ((pointerRingDiameter
                        - pointerRingInnerCircleDiameter) / 2), paintTouchPoint);
        }

        _rendering = false;
    }

    private void CanvasView_OnTouch(object sender, SKTouchEventArgs e)
    {
#if WINDOWS
        if (!e.InContact)
        {
            if (TouchActionType == TouchActionType.OnTouchUp)
            {
                SetValue(PickedColorProperty, _pendingPickedColor);
            }
		    return;
        }
#endif
        // Select the pending color if set to do so on touch up
        if (TouchActionType == TouchActionType.OnTouchUp
            && e.ActionType == SKTouchAction.Released)
        {
            if (_pendingPickedColor is null)
            {
                _pendingPickedColor = _touchPointColor.ToMauiColor();
            }
            SetValue(PickedColorProperty, _pendingPickedColor);
            return;
        }

        var canvasSize = CanvasView.CanvasSize;

        // Check for each touch point XY position to be inside Canvas
        // Ignore any Touch event occured outside the Canvas region 
        if ((e.Location.X > 0 && e.Location.X < canvasSize.Width) &&
            (e.Location.Y > 0 && e.Location.Y < canvasSize.Height))
        {
            e.Handled = true;

            _pendingPickedColor = null;

            // Prevent double re-rendering.
            _rendering = true;
            SetValue(PointerRingPositionXUnitsProperty, e.Location.X / canvasSize.Width);
            SetValue(PointerRingPositionYUnitsProperty, e.Location.Y / canvasSize.Height);
            _rendering = false;

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

public enum TouchActionType
{
    OnTouchDown,
    OnTouchUp
}
