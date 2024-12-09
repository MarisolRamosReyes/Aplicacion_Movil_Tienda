using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIenda_Movil_Fin
{
    public class Product : INotifyPropertyChanged
    {
        private int _stock;

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int Stock
        {
            get => _stock;
            set
            {
                if (_stock != value)
                {
                    _stock = value;
                    OnPropertyChanged(nameof(Stock));
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

        public ImageSource Image { get; set; } = null!;
        public bool IsActive => Stock > 0;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
