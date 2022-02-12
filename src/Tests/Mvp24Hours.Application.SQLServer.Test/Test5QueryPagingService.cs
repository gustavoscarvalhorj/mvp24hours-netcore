//=====================================================================================
// Developed by Kallebe Lins (https://github.com/kallebelins)
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
using Mvp24Hours.Application.SQLServer.Test.Setup;
using Mvp24Hours.Application.SQLServer.Test.Support.Entities;
using Mvp24Hours.Application.SQLServer.Test.Support.Services;
using Mvp24Hours.Core.ValueObjects.Logic;
using Mvp24Hours.Helpers;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Priority;
using Microsoft.Extensions.DependencyInjection;

namespace Mvp24Hours.Application.SQLServer.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Name)]
    public class Test5QueryPagingService : IDisposable
    {
        private readonly Startup startup;
        private readonly IServiceProvider serviceProvider;

        #region [ Ctor ]
        /// <summary>
        /// Initialize
        /// </summary>
        public Test5QueryPagingService()
        {
            startup = new Startup();
            serviceProvider = startup.Initialize();
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            startup.Cleanup(serviceProvider);
        }
        #endregion

        #region [ List ]
        [Fact, Priority(2)]
        public void Get_Filter_Customer_List()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            // act
            var pagingResult = service.ListWithPagination();
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(5)]
        public void Get_Filter_Customer_List_Paging()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteria(3, 0);
            // act
            var pagingResult = service.ListWithPagination(paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(5)]
        public void Get_Filter_Customer_List_Navigation()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteria(3, 0, navigation: new List<string> { "Contacts" });
            // act
            var pagingResult = service.ListWithPagination(paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(6)]
        public void Get_Filter_Customer_List_Order_Asc()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteria(3, 0, new List<string> { "Name" });
            // act
            var pagingResult = service.ListWithPagination(paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(6)]
        public void Get_Filter_Customer_List_Order_Desc()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteria(3, 0, new List<string> { "Name desc" });
            // act
            var pagingResult = service.ListWithPagination(paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(7)]
        public void Get_Filter_Customer_List_Order_Asc_Expression()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteriaExpression<Customer>(3, 0);
            paging.OrderByAscendingExpr.Add(x => x.Name);
            // act
            var pagingResult = service.ListWithPagination(paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(8)]
        public void Get_Filter_Customer_List_Order_Desc_Expression()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteriaExpression<Customer>(3, 0);
            paging.OrderByDescendingExpr.Add(x => x.Name);
            // act
            var pagingResult = service.ListWithPagination(paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(9)]
        public void Get_Filter_Customer_List_Paging_Expression()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteriaExpression<Customer>(3, 0);
            // act
            var pagingResult = service.ListWithPagination(paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(9)]
        public void Get_Filter_Customer_List_Navigation_Expression()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteriaExpression<Customer>(3, 0);
            paging.NavigationExpr.Add(x => x.Contacts);
            // act
            var pagingResult = service.ListWithPagination(paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        #endregion

        #region [ GetBy ]
        [Fact, Priority(2)]
        public void Get_Filter_Customer_GetBy()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            // act
            var pagingResult = service.GetByWithPagination(x => x.Name.Contains("Test"));
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(5)]
        public void Get_Filter_Customer_GetBy_Paging()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteria(3, 0);
            // act
            var pagingResult = service.GetByWithPagination(x => x.Name.Contains("Test"), paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(5)]
        public void Get_Filter_Customer_GetBy_Navigation()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteria(3, 0, navigation: new List<string> { "Contacts" });
            // act
            var pagingResult = service.GetByWithPagination(x => x.Name.Contains("Test"), paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(6)]
        public void Get_Filter_Customer_GetBy_Order_Asc()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteria(3, 0, new List<string> { "Name" });
            // act
            var pagingResult = service.GetByWithPagination(x => x.Name.Contains("Test"), paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(6)]
        public void Get_Filter_Customer_GetBy_Order_Desc()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteria(3, 0, new List<string> { "Name desc" });
            // act
            var pagingResult = service.GetByWithPagination(x => x.Name.Contains("Test"), paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(7)]
        public void Get_Filter_Customer_GetBy_Order_Asc_Expression()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteriaExpression<Customer>(3, 0);
            paging.OrderByAscendingExpr.Add(x => x.Name);
            // act
            var pagingResult = service.GetByWithPagination(x => x.Name.Contains("Test"), paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(8)]
        public void Get_Filter_Customer_GetBy_Order_Desc_Expression()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteriaExpression<Customer>(3, 0);
            paging.OrderByDescendingExpr.Add(x => x.Name);
            // act
            var pagingResult = service.GetByWithPagination(x => x.Name.Contains("Test"), paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(9)]
        public void Get_Filter_Customer_GetBy_Paging_Expression()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteriaExpression<Customer>(3, 0);
            // act
            var pagingResult = service.GetByWithPagination(x => x.Name.Contains("Test"), paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        [Fact, Priority(9)]
        public void Get_Filter_Customer_GetBy_Navigation_Expression()
        {
            // arrange
            var service = serviceProvider.GetService<CustomerPagingService>();
            var paging = new PagingCriteriaExpression<Customer>(3, 0);
            paging.NavigationExpr.Add(x => x.Contacts);
            // act
            var pagingResult = service.GetByWithPagination(x => x.Name.Contains("Test"), paging);
            // assert
            Assert.True(pagingResult.Paging != null);
        }
        #endregion
    }
}

