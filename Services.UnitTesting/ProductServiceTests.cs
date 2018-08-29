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
        private Mock<IProductRepository> _productRepository;
        
        [SetUp]
        public void Setup()
        {
            _productRepository = new Mock<IProductRepository>();

            _productService = new ProductService(_productRepository.Object);
        }

        [Test, AutoData]
        public async Task Contains_Full_Page_Of_Data(Product[] products)
        {
            _productRepository.Setup(repo => repo.GetPageOfProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(products));

            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, products.Length);

            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.Data.Length.ShouldBe(products.Length);
            pageOfProducts.PageSize.ShouldBe(products.Length);
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
