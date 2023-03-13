using DevFramework.Northwind.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevFramework.Northwind.MvcWebUI.Models;
using DevFramework.Northwind.Entities.Concrete;

namespace DevFramework.Northwind.MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var model = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };
            return View(model);
        }

        public string AddUpdate()
        {
            _productService.TransactionalOperation(new Product
            {
                CategoryId =1,
                ProductName ="Gsm",
                QuantityPerUnit ="1",
                UnitPrice = 21
            }, new Product
            {
                CategoryId = 1,
                ProductName = "Gsm",
                QuantityPerUnit = "1",
                UnitPrice = 10
            });
            return "Done";
        }
    }
}