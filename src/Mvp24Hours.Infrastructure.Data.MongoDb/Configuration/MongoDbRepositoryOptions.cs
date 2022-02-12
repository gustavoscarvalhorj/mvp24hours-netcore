﻿//=====================================================================================
// Developed by Kallebe Lins (https://github.com/kallebelins)
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
using Mvp24Hours.Core.Helpers;

namespace Mvp24Hours.Infrastructure.Data.MongoDb.Configuration
{
    public sealed class MongoDbRepositoryOptions
    {
        public int MaxQtyByQueryPage { get; set; } = ContantsHelper.Data.MaxQtyByQueryPage;
    }
}
