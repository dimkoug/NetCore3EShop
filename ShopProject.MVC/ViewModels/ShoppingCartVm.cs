using ShopProject.MVC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.MVC.ViewModels
{
    public class ShoppingCartVm
    {
        public ShoppingCartHelper ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
