using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using DomainModel;
using Moq;
using NUnit.Framework;
using Queries;
using Shouldly;

namespace RequestHandlers.UnitTesting
{
    [TestFixture]
    public class GetAllProductsPagedTests
    {
        private GetAllProductsPaged _getAllProductsPaged;
        private Mock<IQueryHandler<AllProductsPagedQry, Product[]>> _qryAllProductsPaged;
        private Mock<IQueryHandler<ProductsCountQry, int>> _qryProductsCount;
        private Product[] products;

        [SetUp]
        public void Setup()
        {
            _qryAllProductsPaged = new Mock<IQueryHandler<AllProductsPagedQry, Product[]>>();
            _qryProductsCount = new Mock<IQueryHandler<ProductsCountQry, int>>();

            products = new Fixture().CreateMany<Product>(20).ToArray();

            _getAllProductsPaged = new GetAllProductsPaged(_qryAllProductsPaged.Object, _qryProductsCount.Object);
        }

        void SetPageSize(int pageSize)
        {
            _qryAllProductsPaged.Setup(handler =>
                    handler.FetchAsync(It.IsAny<AllProductsPagedQry>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(products.Take(pageSize).ToArray()));
        }

        void SetTotalRecords(int totalRecords)
        {
            _qryProductsCount.Setup(handler =>
                    handler.FetchAsync(It.IsAny<ProductsCountQry>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(totalRecords));
        }

        [Test]
        public async Task Returns_All_Data_In_One_Page()
        {
            var pageSize = products.Length + 1;
            
            SetPageSize(pageSize);
            SetTotalRecords(pageSize);

            var pagedData = await _getAllProductsPaged.HandleAsync(new Requests.GetAllProductsPagedRequest
            {
                PageSize = pageSize, // fetch all the data in one page
                Offset = 0
            });

            pagedData.ShouldNotBeNull();
            pagedData.TotalRecords.ShouldBe(products.Length);
            pagedData.Data.Length.ShouldBe(products.Length);
        }

        [Test]
        public async Task Returns_One_Page_of_Overall_Data()
        {
            var pageSize = products.Length / 2;

            SetPageSize(pageSize);
            SetTotalRecords(products.Length);

            var pagedData = await _getAllProductsPaged.HandleAsync(new Requests.GetAllProductsPagedRequest
            {
                PageSize = pageSize, // fetch all the data in one page
                Offset = 0
            });

            pagedData.ShouldNotBeNull();
            pagedData.TotalRecords.ShouldBe(products.Length);
            pagedData.Data.Length.ShouldBe(pageSize);
        }
    }
}
