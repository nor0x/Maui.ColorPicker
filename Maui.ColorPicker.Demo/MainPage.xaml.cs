namespace Maui.ColorPicker.Demo;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    private void ColorPicker_PickedColorChanged(object sender, Color colorPicked)
    {
        // Use the selected color
        SelectedColorDisplayFrame.BackgroundColor = colorPicked;
        SelectedColorValueLabel.Text = colorPicked.ToHex();
        SelectedColorValueLabel.BackgroundColor = colorPicked;
        ColorPickerHolderFrame.BackgroundColor = colorPicked;

        if (colorPicked.GetLuminosity() < 0.5)
            SelectedColorValueLabel.TextColor = Colors.White;
        else
            SelectedColorValueLabel.TextColor = Colors.SlateGray;
    }
}