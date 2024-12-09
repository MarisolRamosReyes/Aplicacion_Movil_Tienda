using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TIenda_Movil_Fin
{
    public class ProductService : INotifyPropertyChanged
    {
        private List<Product> _products;
        private ObservableCollection<CartItem> _cart;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Cafetera", Price = 150, Stock = 10, Image = ImageSource.FromFile("cafetera.jpg") },
                new Product { Id = 2, Name = "Aspiradora", Price = 100, Stock = 5, Image = ImageSource.FromFile("aspiradora.jpg") },
                new Product { Id = 3, Name = "Cama", Price = 300, Stock = 3, Image = ImageSource.FromFile("cama.png") },
                new Product { Id = 4, Name = "Batidora", Price = 200, Stock = 12, Image = ImageSource.FromFile("batidora.jpg") },
                new Product { Id = 5, Name = "Escritorio", Price = 250, Stock = 8, Image = ImageSource.FromFile("escritorio.jpeg") }
            };

            _cart = new ObservableCollection<CartItem>();
        }

        public List<Product> GetProducts() => _products;
        public ObservableCollection<CartItem> GetCartItems() => _cart;
        public int CartCount => _cart.Sum(c => c.Quantity);
        public decimal TotalPrice => _cart.Sum(c => c.Quantity * c.Product.Price);

        public void AddToCart(Product product, int quantity)
        {
            if (product.Stock > 0)
            {
                var existingProduct = _cart.FirstOrDefault(p => p.Product.Id == product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Quantity += quantity;
                }
                else
                {
                    _cart.Add(new CartItem { Product = product, Quantity = quantity });
                }
                product.Stock -= quantity;
            }
        }

        public void ClearCart()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Cafetera", Price = 150, Stock = 10, Image = ImageSource.FromFile("cafetera.jpg") },
                new Product { Id = 2, Name = "Aspiradora", Price = 100, Stock = 5, Image = ImageSource.FromFile("aspiradora.jpg") },
                new Product { Id = 3, Name = "Cama", Price = 300, Stock = 3, Image = ImageSource.FromFile("cama.png") },
                new Product { Id = 4, Name = "Batidora", Price = 200, Stock = 12, Image = ImageSource.FromFile("batidora.jpg") },
                new Product { Id = 5, Name = "Escritorio", Price = 250, Stock = 8, Image = ImageSource.FromFile("escritorio.jpeg") }
            };

            _cart.Clear();
            OnPropertyChanged(nameof(CartCount));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
