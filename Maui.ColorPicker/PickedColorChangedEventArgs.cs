using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.ColorPicker;

/// <summary>
/// Event arguments for PickedColorChanged events. Provides old and new picked color values.
/// </summary>
public class PickedColorChangedEventArgs : EventArgs
{
	/// <summary>
	/// Creates a new <see cref="PickedColorChangedEventArgs"/> with <paramref name="oldColorValue"/>
	/// and <paramref name="newColorValue"/>
	/// </summary>
	/// <param name="oldColorValue">The old picked color value.</param>
	/// <param name="newColorValue">The new picked color value.</param>
	public PickedColorChangedEventArgs(Color? oldColorValue, Color newColorValue)
	{
		OldPickedColorValue = oldColorValue;
		NewPickedColorValue = newColorValue;
	}

	/// <summary>
	/// Gets the new picked color value.
	/// </summary>
	public Color NewPickedColorValue { get; init; }

	/// <summary>
	/// Gets the old picked color value.
	/// </summary>
	public Color? OldPickedColorValue { get; init; }
}