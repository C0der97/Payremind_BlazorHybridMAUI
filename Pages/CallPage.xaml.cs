using CommunityToolkit.Mvvm.Messaging;
using PayRemind.Data;
using PayRemind.Messages;


namespace PayRemind.Pages;

public partial class CallPage : ContentPage
{
	public CallPage(string incomingNumber)
	{
		InitializeComponent();
        InfoStatic.PhoneNumber = incomingNumber;


        WeakReferenceMessenger.Default.Register<TabIndexMessage>(this, (r, message) =>
        {
            if (message != null && message.CloseCallPage)
            {
                // Navegar de vuelta cuando la llamada se cuelgue
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                });
            }
        });
    }

    void OnAnswerButtonClicked(object sender, EventArgs e)
    {
        // Lógica para responder la llamada
    }

    void OnRejectButtonClicked(object sender, EventArgs e)
    {
        // Lógica para rechazar la llamada
    }
}