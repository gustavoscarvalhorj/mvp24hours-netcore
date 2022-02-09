//=====================================================================================
// Developed by Kallebe Lins (https://github.com/kallebelins)
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
using Mvp24Hours.Application.Logic;
using Mvp24Hours.Application.PostgreSql.Test.Support.Entities;
using Mvp24Hours.Core.Contract.Data;

namespace Mvp24Hours.Application.PostgreSql.Test.Support.Services.Async
{
    public class CustomerPagingServiceAsync : RepositoryPagingServiceAsync<Customer, IUnitOfWorkAsync>
    {
        // custom methods here
    }
}
