namespace TIenda_Movil_Fin;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnContactPageButtonClicked(object sender, EventArgs e)
    {
        var productsPage = new ProductsPage(App.Current!.Handler.MauiContext!.Services.GetService<ProductService>()!);
        await Navigation.PushAsync(productsPage);
        //await Navigation.PushAsync(new ContactPage());
    }

    private async void OnAboutPageButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AboutPage());
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