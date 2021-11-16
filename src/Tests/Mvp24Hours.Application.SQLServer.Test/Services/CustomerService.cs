﻿//=====================================================================================
// Developed by Kallebe Lins (kallebe.santos@outlook.com)
// Teacher, Architect, Consultant and Project Leader
// Virtual Card: https://www.linkedin.com/in/kallebelins
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
using Mvp24Hours.Application.SQLServer.Test.Entities;
using Mvp24Hours.Business.Logic;
using Mvp24Hours.Core.Contract.Data;
using Mvp24Hours.Core.ValueObjects.Logic;
using System.Collections.Generic;
using System.Linq;

namespace Mvp24Hours.Application.SQLServer.Test.Services
{
    public class CustomerService : RepositoryService<Customer, IUnitOfWork>
    {
        // custom methods here

        public IList<Customer> GetWithContacts()
        {
            var paging = new PagingCriteria(3, 0);

            var rpCustomer = UnitOfWork.GetRepository<Customer>();

            var customers = rpCustomer.GetBy(x => x.Contacts.Any(), paging);

            foreach (var customer in customers)
            {
                rpCustomer.LoadRelation(customer, x => x.Contacts);
            }
            return customers;
        }

        public IList<Customer> GetWithPagedContacts()
        {
            var paging = new PagingCriteria(3, 0);

            var rpCustomer = UnitOfWork.GetRepository<Customer>();

            var customers = rpCustomer.GetBy(x => x.Contacts.Any(), paging);

            foreach (var customer in customers)
            {
                rpCustomer.LoadRelation(customer, x => x.Contacts, limit: 1);
            }
            return customers;
        }
    }
}
