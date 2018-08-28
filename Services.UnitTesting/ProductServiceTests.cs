﻿using System;
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
            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, 10);

            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.Data.Length.ShouldBe(10);
            pageOfProducts.PageSize.ShouldBe(10);
        }

        [Test, AutoData]
        public async Task Contains_Partial_Page_Of_Data(Product[] products)
        {
            // Setup ProductRepository to return products

            var pageSize = products.Length * 2;
            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, pageSize);

            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.PageSize.ShouldBe(pageSize);
            pageOfProducts.Data.Length.ShouldBe(products.Length);
        }

        [Test, AutoData]
        public async Task Accurate_Total_Record_Count(Product[] products)
        {
            // Setup ProductRepository to return products

            int totalRecords = 10;

            // Setup ProductRepository to return totalrecords

            PagedData<Product> pageOfProducts = await _productService.GetAllProductsPagedAsync(0, 0);
            pageOfProducts.ShouldNotBeNull();
            pageOfProducts.TotalRecords.ShouldBe(totalRecords);
        }
    }
}
