using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ProductID=1,Name="P1"},
                new Product{ProductID=2,Name="P2"},
                new Product{ProductID=3,Name="P3"},
                new Product{ProductID=4,Name="P4"},
                new Product{ProductID=5,Name="P5"}
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //动作
            ProductListViewModel result = (ProductListViewModel)controller.List(2).Model;

            Product[] productArray = result.Products.ToArray();
            //断言
            Assert.IsTrue(productArray.Length == 2);
            Assert.AreEqual(productArray[0].Name, "P4");
            Assert.AreEqual(productArray[1].Name, "P5");
            Assert.AreEqual(2, result.PagingInfo.CurrentPage);
            Assert.AreEqual(5, result.PagingInfo.TotalItems);
            Assert.AreEqual(2, result.PagingInfo.TotalPage);
            Assert.AreEqual(3, result.PagingInfo.ItemsPerPage);
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper helper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                ItemsPerPage = 4,
                TotalItems = 10,
            };

             MvcHtmlString result = helper.PageLinks(pagingInfo, i => "Page" + i);
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a><a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a><a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
            

        }
    }
}
