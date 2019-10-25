using FundConnRec.API.Controllers;
using FundConnRec.API.Models;
using FundConnRec.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FundConnRec.UnitTests
{
    public class APIProductControllerTests
    {
        /*[Theory]
        [InlineData("chleb",2.50)]
        public async Task AddProductTest(string name, decimal price)
        {
            var options = new DbContextOptionsBuilder<FundConnContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            var context = new FundConnContext(options);
            ProductsController controller = new ProductsController(context);
            Product product = new Product(name, price);

            await controller.PostProduct(product);

            Assert.Equal(context.Products.Where(x => x.Name == product.Name).FirstOrDefault().Name,product.Name);
            Assert.Equal(context.Products.Where(x => x.Price == product.Price).FirstOrDefault().Price, product.Price);
        }*/
    }
}
