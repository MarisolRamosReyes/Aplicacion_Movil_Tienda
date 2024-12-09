using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TIenda_Movil_Fin
{
    public class ProductService
    {
        private List<Product> _products;
        private List<CartItem> _cart;

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

            _cart = new List<CartItem>();
        }

        public List<Product> GetProducts() => _products;

        public List<CartItem> GetCartItems() => _cart;

        public void AddToCart(Product product, int quantity)
        {
            if (product.Stock > 0)
            {
                var existingProduct = _cart.FirstOrDefault(p => p.Product.Id == product.Id);
                if (existingProduct != null)
                {
                    if (product.Stock > 0)
                        existingProduct.Quantity += quantity;
                }
                else
                {
                    _cart.Add(new CartItem { Product = product, Quantity = quantity });
                }
                product.Stock--;
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

            _cart = new List<CartItem>();
        }
    }
}
