namespace TIenda_Movil_Fin;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnContactPageButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ContactPage());
    }

    private async void OnAboutPageButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AboutPage());
    }
    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    { 
        bool confirm = await DisplayAlert("Cerrar Sesi�n", "�Est�s seguro de que deseas cerrar la sesi�n?", "S�", "No");
        if (confirm)
        {
            await Navigation.PopToRootAsync();
        }
    }

}