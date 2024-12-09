using System.Collections.ObjectModel;

namespace TIenda_Movil_Fin;

public partial class ProductsPage : ContentPage
{
    private readonly ProductService _productService;

    public ProductsPage(ProductService productService)
	{
		InitializeComponent();
        _productService = productService;
        Refresh();
    }

    private void Refresh()
    {
        var products = _productService.GetProducts();
        if (products == null || !products.Any())
        {
            DisplayAlert("Error", "El servicio no contiene productos. Puede haberse eliminado.", "OK");
        }
        else
            ProductsListView.ItemsSource = products;
    }

    private void OnAddToCartClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var product = (Product)button.BindingContext;
        _productService.AddToCart(product, 1);
        DisplayAlert("Carrito", $"{product.Name} agregado al carrito", "OK");
        Refresh();
    }

    private async void OnCartButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CartPage(_productService));
    }
}