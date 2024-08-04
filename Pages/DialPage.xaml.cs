namespace PayRemind.Pages;

public partial class DialPage : ContentPage
{
	public DialPage()
	{
		InitializeComponent();
	}

    private async void OnDialButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(numberEntry.Text))
        {
            await DisplayAlert("Error", "Please enter a phone number", "OK");
            return;
        }

        try
        {
            if (PhoneDialer.Default.IsSupported)
            {
                PhoneDialer.Default.Open(numberEntry.Text);
            }
            else
            {
                await DisplayAlert("Not Supported", "Phone dialing is not supported on this device", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to dial: {ex.Message}", "OK");
        }
    }
}