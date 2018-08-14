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
        private Mock<IQueryHandler<Queries.ProductsCountQry, int>> _qryProductsCount;
        private Product[] products;

        [SetUp]
        public void Setup()
        {
            _qryAllProductsPaged = new Mock<IQueryHandler<AllProductsPagedQry, Product[]>>();
            _qryProductsCount = new Mock<IQueryHandler<ProductsCountQry, int>>();

            products = new Fixture().CreateMany<Product>(20).ToArray();

            _qryProductsCount.Setup(handler =>
                    handler.FetchAsync(It.IsAny<Queries.ProductsCountQry>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(products.Length));

            _qryAllProductsPaged.Setup(handler =>
                    handler.FetchAsync(It.IsAny<AllProductsPagedQry>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(products));

            _getAllProductsPaged = new GetAllProductsPaged(_qryAllProductsPaged.Object, _qryProductsCount.Object);
        }

        [Test]
        public async Task Returns_All_Data_In_One_Page()
        {
            var pagedData = await _getAllProductsPaged.HandleAsync(new Requests.GetAllProductsPagedRequest
            {
                PageSize = products.Length + 1, // fetch all the data in one page
                Offset = 0
            });

            pagedData.ShouldNotBeNull();
            pagedData.TotalRecords.ShouldBe(products.Length);
            pagedData.Data.Length.ShouldBe(products.Length);
        }
    }
}
