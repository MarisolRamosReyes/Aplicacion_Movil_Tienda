namespace TIenda_Movil_Fin;

public partial class HomePage : ContentPage
{
    private readonly ProductService _productService;

    public HomePage()
    {
        InitializeComponent();
        _productService = new ProductService();
    }

    private async void OnContactPageButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductsPage(_productService));
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
            Application.Current!.MainPage = new LoginPage();
        }
    }

}