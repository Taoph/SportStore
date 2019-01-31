using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }
        // GET: Product
        public ViewResult List(int page=1)
        {

            ProductListViewModel model = new ProductListViewModel
            {
                PagingInfo = new PagingInfo
                {
                    TotalItems = repository.Products.Count(),
                    CurrentPage = page,
                    ItemsPerPage = PageSize
                },
                Products = repository.Products.OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize)
            };
            return View(model);
        }
    }
}