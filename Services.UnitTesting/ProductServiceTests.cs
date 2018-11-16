using System;
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
    public class ProductService_GetPageOfProductsAsyncTests
    {
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _productService = new ProductService();
        }

        [Test]
        public async Task Contains_Full_Page_Of_Data()
        {
            // TODO: ISSUE: This will not run on CI server (hits db)
            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, 10);

            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.Data.Length.ShouldBe(10);
            pageOfProducts.PageSize.ShouldBe(10);
        }

        [Test, AutoData]
        public async Task Contains_Partial_Page_Of_Data(Product[] products)
        {
            // TODO: ISSUE: Can't control response from ProductRepository.GetPageOfProductsAsync

            var pageSize = products.Length * 2;
            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, pageSize);

            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.PageSize.ShouldBe(pageSize);
            pageOfProducts.Data.Length.ShouldBe(products.Length);
        }

        [Test, AutoData]
        public async Task Accurate_Total_Record_Count(Product[] products)
        {
            // TODO: ISSUE: Can't control response from ProductRepository.GetPageOfProductsAsync

            int totalRecords = 10;

            // TODO: ISSUE: Can't control response from ProductRepository.GetAllProductsCountAsync

            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, 0);
            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.TotalRecords.ShouldBe(totalRecords);
        }

        [Test]
        public async Task Exceptions_Logged()
        {
            var exceptionLogged = false;

            await Should.ThrowAsync<Exception>(async () => await _productService.GetAllProductsPagedAsync(0, 0));

            // TODO: ISSUE: How to verify excption was logged?


            exceptionLogged.ShouldBeTrue();
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
