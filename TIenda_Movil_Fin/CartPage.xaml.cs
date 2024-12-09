using System.Collections.ObjectModel;

namespace TIenda_Movil_Fin;

public partial class CartPage : ContentPage
{
    private readonly ProductService _productService;
    public decimal TotalPriece = 0;

    public CartPage(ProductService productService)
    {
        InitializeComponent();
        _productService = productService;
        CartListView.ItemsSource = _productService.GetCartItems();
        UpdateTotalLabel();
    }

    private void UpdateTotalLabel()
    {
        var totalPrice = _productService.TotalPrice;
        var totalLabel = this.FindByName<Label>("TotalLabel");
        totalLabel.Text = $"Total: ${totalPrice}";
    }

    private void OnClearCartClicked(object sender, EventArgs e)
    {
        _productService.ClearCart();
        DisplayAlert("Carrito", "El carrito se ha vaciado", "OK");
        ReturnedBack();
    }

    private async void ReturnedBack()
    {
        await Navigation.PopAsync();
    }

    private void OnRealizeBuy(object sender, EventArgs e)
    {
        _productService.ClearCart();
        DisplayAlert("Carrito", "Compra realizada", "OK");
        ReturnedHome();
    }

    private async void ReturnedHome()
    {
        await Navigation.PopToRootAsync();
    }

}