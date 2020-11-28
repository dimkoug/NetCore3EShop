using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.MVC.Controllers
{
    [Authorize]
    public class CmsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
