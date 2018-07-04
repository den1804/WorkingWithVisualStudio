using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WorkingWithVisualStudio.Contrellers;
using WorkingWithVisualStudio.Models;
using Xunit;

namespace WorkingWithVisualStudio.Tests
{
    public class HomeControllerTests
    {
        class ModelCompleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; set; }

            public void AddProduct(Product p)
            {

            }
        }
        // параметризированный тест 
        [Theory]
        [InlineData(275, 48.95, 19.50, 24.95)]
        [InlineData(5, 48.95, 19.50, 24.95)]
        [InlineData(5, 48.95, 19.50, -1)]
        public void IndexActionModelIsComplete(decimal price1, decimal price2,
            decimal price3, decimal price4)
        {
            // Arrange 
            var contreller = new HomeController
            {
                repository = new ModelCompleteFakeRepository
                {
                    Products = new Product[]
                    {
                        new Product {Name = "P1", Price = price1},
                        new Product {Name = "P2", Price = price2},
                        new Product {Name = "P3", Price = price3},
                        new Product {Name = "P4", Price = price4}
                    }
                }
            };

            // Act 
            var model = (contreller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // Assert
            Assert.Equal(contreller.repository.Products, model, Comparer.Get<Product>
                ((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }

        // Параметризированный тест классом 
        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsCompleteClassData(Product[] products)
        {
            //Arrange 
            var controller = new HomeController
            {
                repository = new ModelCompleteFakeRepository { Products = products }
            };

            //Act 
            var model = (controller.Index() as ViewResult)?.Model as IEnumerable<Product>;

            //Assert
            Assert.Equal(controller.repository.Products, model, Comparer.Get<Product>(
                (p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }

        class PropertyOnceFakeRepository : IRepository
        {
            public int PropertyCounter { get; set; } = 0;
            public IEnumerable<Product> Products
            {
                get
                {
                    PropertyCounter++;
                    return new[] { new Product { Name = "P1", Price = 100 } };
                }
            }
            public void AddProduct(Product p)
            {

            }
        }

        [Fact]
        public void ReposytoryPropertyCalledOnce()
        {
            // Arrange
            var repo = new PropertyOnceFakeRepository();
            var controller = new HomeController { repository = repo };

            // Act 
            var result = controller.Index();

            // Assert
            Assert.Equal(1, repo.PropertyCounter);
        }

        #region Создание не параметризированного теста 

        // так же отпадает надобность в этом фейковом репозитории с жестко забитыми параметрами цены
        //class ModelCompleteFakeRepositoryPricesUnder50 : IRepository
        //{
        //    public IEnumerable<Product> Products { get; } = new Product[]
        //    {
        //        new Product {Name = "P1", Price = 5M},
        //        new Product {Name = "P2", Price = 48.95M},
        //        new Product {Name = "P3", Price = 19.50M},
        //        new Product {Name = "P4", Price = 34.95M},
        //    };
        //    public void AddProduct(Product p)
        //    {

        //    }
        //}

        // Тест работает, но не удобен так - как нет параметров и придется писать подкаждый подобный свой класс с фейковым репозиторием ModelCompleteFakeRepositoryPricesUnder50
        //[Fact]
        //public void IndexActionModelIsComplete()
        //{
        //    // Arrange
        //    var controller = new HomeController();
        //    controller.repository = new ModelCompleteFakeRepositoryPricesUnder50();

        //    //Act 
        //    var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

        //    // Assert 
        //    Assert.Equal(controller.repository.Products, model, Comparer.Get<Product>
        //        ((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));

        //}
        #endregion
    }
}
