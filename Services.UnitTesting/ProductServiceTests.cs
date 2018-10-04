using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using DomainModel;
using Logging;
using Moq;
using NUnit.Framework;
using Repositories;
using Shouldly;

namespace Services.UnitTesting
{
    [TestFixture]
    public class ProductService_GetPageOfProductsAsyncTests
    {
        private ProductService _productService;
        private Mock<IProductRepository> _productRepository;
        private Mock<ILogger> _logger;

        [SetUp]
        public void Setup()
        {
            _productRepository = new Mock<IProductRepository>();
            _logger = new Mock<ILogger>();

            _productService = new ProductService(_productRepository.Object, _logger.Object);
        }

        [Test, AutoData]
        public async Task Contains_Full_Page_Of_Data(Product[] products)
        {
            _productRepository
                .Setup(repo => repo.GetPageOfProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(products));
            var pageSize = 10;

            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, products.Length);

            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.Data.Length.ShouldBe(products.Length);
            pageOfProducts.PageSize.ShouldBe(products.Length);
        }

        [Test, AutoData]
        public async Task Contains_Partial_Page_Of_Data(Product[] products)
        {
            _productRepository
                .Setup(repo => repo.GetPageOfProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(products));

            var pageSize = products.Length * 2;
            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, pageSize);

            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.PageSize.ShouldBe(pageSize);
            pageOfProducts.Data.Length.ShouldBe(products.Length);
        }

        [Test, AutoData]
        public async Task Accurate_Total_Record_Count(Product[] products, int totalRecords)
        {
            _productRepository
                .Setup(repo => repo.GetPageOfProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(products));

            _productRepository
                .Setup(repo => repo.GetAllProductsCountAsync())
                .Returns(Task.FromResult(totalRecords));

            PagedData<Product> pageOfProducts =
                await _productService.GetAllProductsPagedAsync(It.IsAny<int>(), It.IsAny<int>());

            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.TotalRecords.ShouldBe(totalRecords);
        }

        [Test, AutoData]
        public async Task Max_Page_Size_Enforced()
        {
            await Should.ThrowAsync<MaxPageSizeExceededException>(async () => await
                _productService.GetAllProductsPagedAsync(It.IsAny<int>(), 101));
        }

        [Test]
        public async Task Exceptions_Logged()
        {
            var errMsg = "failure";
            _productRepository.Setup(repo => repo.GetPageOfProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception(errMsg));

            await Should.ThrowAsync<Exception>(async () => await _productService.GetAllProductsPagedAsync(0, 0));

            // TODO: ISSUE: How to verify excption was logged?
            _logger.Verify(logger => logger.Exception(It.Is<Exception>(e => e.Message.Equals(errMsg)), It.IsAny<string>()));
        }

        [Test]
        public async Task Beginning_Verbose_Message_Logged()
        {
            var verbosMessageLogged = false;
            await _productService.GetAllProductsPagedAsync(0, 0);

            
            // TODO: ISSUE: How to verify verbos message was logged?


            verbosMessageLogged.ShouldBeTrue();
        }
    }
}
