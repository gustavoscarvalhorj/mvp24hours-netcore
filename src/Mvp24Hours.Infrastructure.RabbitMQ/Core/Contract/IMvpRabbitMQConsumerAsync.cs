//=====================================================================================
// Developed by Kallebe Lins (https://github.com/kallebelins)
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================

using System;
using System.Threading.Tasks;

namespace Mvp24Hours.Infrastructure.RabbitMQ.Core.Contract
{
    public interface IMvpRabbitMQConsumerAsync : IMvpRabbitMQConsumer
    {
        Task ReceivedAsync(object message, string token);
        Task FailureAsync(Exception exception, string token);
        Task RejectedAsync(object message, string token);
    }
}
