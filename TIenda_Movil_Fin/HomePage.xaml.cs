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
    private AboutPage _aboutPage;
    private async void OnAboutPageButtonClicked(object sender, EventArgs e)
    {
        if (_aboutPage == null)
        {
            _aboutPage = new AboutPage();
        }

        await Navigation.PushAsync(_aboutPage);
    }
    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    { 
        bool confirm = await DisplayAlert("Cerrar Sesión", "¿Estás seguro de que deseas cerrar la sesión?", "Sí", "No");
        if (confirm)
        {
            await Navigation.PopToRootAsync();
        }
    }

}