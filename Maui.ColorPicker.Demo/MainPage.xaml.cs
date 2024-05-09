namespace Maui.ColorPicker.Demo;

public partial class MainPage : ContentPage
{
	private bool _valueChanging = false;

	public MainPage()
	{
		InitializeComponent();
		_valueChanging = true;
		SelectedColorValueEntry.Text = ColorPicker.PickedColor.ToHex();
		SelectedColorValueEntry.TextColor = ColorPicker.PickedColor.GetComplementary();
		SelectedColorValueEntry.Background = ColorPicker.PickedColor;
		_valueChanging = false;
	}

	private void ColorPicker_PickedColorChanged(object sender, PickedColorChangedEventArgs e)
	{
		// This check is requried as the function may be called really early in the
		// app's lifetime.
		if (SelectedColorValueEntry != null && !_valueChanging)
		{
			_valueChanging = true;
			// Use the selected color
			SelectedColorValueEntry.Text = e.NewPickedColorValue.ToHex();
			SelectedColorValueEntry.TextColor = e.NewPickedColorValue.GetComplementary();
			SelectedColorValueEntry.Background = e.NewPickedColorValue;
			_valueChanging = false;
		}
	}

	private void SelectedColorValueEntry_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (ColorPicker != null && !_valueChanging)
		{
			if (Color.TryParse(SelectedColorValueEntry.Text, out var color))
			{
				_valueChanging = true;
				ColorPicker.PickedColor = color;
				SelectedColorValueEntry.TextColor = color.GetComplementary();
				SelectedColorValueEntry.Background = color;
				_valueChanging = false;
			}
		}
	}
}