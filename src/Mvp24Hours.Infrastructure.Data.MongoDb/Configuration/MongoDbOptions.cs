﻿namespace Mvp24Hours.Infrastructure.Data.MongoDb.Configuration
{
    public sealed class MongoDbOptions
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public bool EnableTls { get; set; }
        public bool EnableTransaction { get; set; }
    }
}
