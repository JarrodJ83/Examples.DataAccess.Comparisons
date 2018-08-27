using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using DomainModel;
using Moq;
using NUnit.Framework;
using Repositories;
using Shouldly;

namespace Services.UnitTesting
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ProductService _productService;
        private Mock<IProductRepository> _productRepository;
        
        [SetUp]
        public void Setup()
        {
            _productRepository = new Mock<IProductRepository>();

            _productService = new ProductService(_productRepository.Object);
        }

        [Test]
        public async Task Contains_Full_Page_Of_Data()
        {
            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, 10);

            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.Data.Length.ShouldBe(10);
            pageOfProducts.PageSize.ShouldBe(10);
        }

        [Test, AutoData]
        public async Task Contains_Partial_Page_Of_Data(Product[] products)
        {
            _productRepository.Setup(repo => repo.GetPageOfProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(products));

            var pageSize = products.Length * 2;
            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, pageSize);

            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.PageSize.ShouldBe(pageSize);
            pageOfProducts.Data.Length.ShouldBe(products.Length);
        }

        [Test, AutoData]
        public async Task Accurate_Total_Record_Count(Product[] products)
        {
            _productRepository.Setup(repo => repo.GetPageOfProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(products));

            int totalRecords = 10;
            _productRepository.Setup(repo => repo.GetAllProductsCountAsync())
                .Returns(Task.FromResult(totalRecords));

            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, 0);
            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.TotalRecords.ShouldBe(totalRecords);
        }
    }
}
