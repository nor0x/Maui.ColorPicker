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
        SelectedColorValueLabel.Text = colorPicked.ToHex();
        SelectedColorValueLabel.Background = colorPicked;
    }
}