using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<ProductModel> _products;
        private BindingList<string> _cart;
        private int _itemQuantity;
        
        private IProductEndPoint _productEndPoint;
        public SalesViewModel(IProductEndPoint productEndPoint)
        {
            _productEndPoint = productEndPoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }
        private async Task LoadProducts()
        {
            var productList = await _productEndPoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }


        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }
        public BindingList<string> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public string SubTotal
        {
            get { return "$0.00"; }
        }
        public string Tax
        {
            get { return "$0.00"; }
        }
        public string Total
        {
            get { return "$0.00"; }
        }

        public bool CanAddToCart
        {
            get
            {
                return false;
            }
        }
        public void AddToCart()
        {

        }

        public bool CanRemoveFromCart
        {
            get
            {
                return false;
            }
        }
        public void RemoveFromCart()
        {

        }

        public bool CanCheckout
        {
            get
            {
                return false;
            }
        }
        public void CheckOut()
        {

        }
    }
}
