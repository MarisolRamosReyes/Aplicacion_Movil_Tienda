using System.Collections.ObjectModel;

namespace TIenda_Movil_Fin;

public partial class CartPage : ContentPage
{
    private readonly ProductService _productService;

    public CartPage(ProductService productService)
    {
        InitializeComponent();
        _productService = productService;
        CartListView.ItemsSource = _productService.GetCartItems();
    }

    private void OnClearCartClicked(object sender, EventArgs e)
    {
        _productService.ClearCart();
        CartListView.ItemsSource = null;
        DisplayAlert("Carrito", "El carrito se ha vaciado", "OK");
        ReturnedBack();
    }

    private async void ReturnedBack()
    {
        await Navigation.PopAsync();
    }

    private void OnRealizeBuy(object sender, EventArgs e)
    {
        //Pendiente proceso de venta
        ReturnedHome();
    }

    private async void ReturnedHome()
    {
        await Navigation.PopToRootAsync();
    }

}