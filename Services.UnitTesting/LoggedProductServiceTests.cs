using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Logging;
using Moq;
using NUnit.Framework;
using Services.Core;
using Shouldly;

namespace Services.UnitTesting
{
    [TestFixture]
    public class LoggedProductServiceTests
    {
        private LoggedProductService _loggedProductService;
        private Mock<IProductService> _productService;
        private Mock<ILogger> _logger;

        [SetUp]
        public void Setup()
        {
            _productService = new Mock<IProductService>();
            _logger = new Mock<ILogger>();
            _loggedProductService = new LoggedProductService(_logger.Object, _productService.Object);
        }

        [Test]
        public async Task Exceptions_Logged()
        {
            _logger.Setup(logger => logger.Verbose(It.IsAny<string>()));
            _logger.Setup(logger => logger.Exception(It.IsAny<Exception>(), It.IsAny<string>()));

            _productService.Setup(svc => svc.GetAllProductsPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception("test"));

            await Should.ThrowAsync<Exception>(async () => await _loggedProductService.GetAllProductsPagedAsync(0, 0));

            _logger.Verify(logger => logger.Exception(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task Beginning_Verbose_Message_Logged()
        {
            _logger.Setup(logger => logger.Verbose(It.IsAny<string>()));
            _logger.Setup(logger => logger.Exception(It.IsAny<Exception>(), It.IsAny<string>()));

            _productService.Setup(svc => svc.GetAllProductsPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new PagedData<Product>()));

            await _loggedProductService.GetAllProductsPagedAsync(0, 0);

            _logger.Verify(logger => logger.Verbose(It.IsAny<string>()), Times.Once);
        }

    }
}
