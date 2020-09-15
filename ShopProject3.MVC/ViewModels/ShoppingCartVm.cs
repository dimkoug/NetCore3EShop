using ShopProject3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject3.MVC.ViewModels
{
    public class ShoppingCartVm
    {
        public ShoppingCartHelper ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
