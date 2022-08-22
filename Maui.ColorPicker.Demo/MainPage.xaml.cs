namespace Maui.ColorPicker.Demo;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        ColorPicker.ColorList = new string[] 
        {
            "#ff0000",
            "#ffff00",
            "#00ff00",
            "#00ffff",
            "#0000ff",
            "#ff00ff",
            "#ff0000",
        };
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