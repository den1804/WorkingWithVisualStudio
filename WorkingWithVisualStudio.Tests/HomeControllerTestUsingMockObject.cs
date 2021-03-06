﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using WorkingWithVisualStudio.Models;
using WorkingWithVisualStudio.Contrellers;
using Microsoft.AspNetCore.Mvc;

namespace WorkingWithVisualStudio.Tests
{
    public class HomeControllerTestUsingMockObject
    {
        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsComplete(Product[] products)
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(products);
            var controller = new HomeController { repository = mock.Object };

            // Act 
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // Assert
            Assert.Equal(controller.repository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }

        [Fact]
        public void RepositoryPropertyCalledOcne()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products)
                .Returns(new[] { new Product { Name = "P1", Price = 100 } });
            var controller = new HomeController { repository = mock.Object };

            // Act 
            var result = controller.Index();

            // Assert
            mock.VerifyGet(m => m.Products, Times.Once);
        }
    }
}
