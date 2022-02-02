//=====================================================================================
// Developed by Kallebe Lins (kallebe.santos@outlook.com)
// Teacher, Architect, Consultant and Project Leader
// Virtual Card: https://www.linkedin.com/in/kallebelins
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
using Mvp24Hours.Core.ValueObjects.RabbitMQ;
using Mvp24Hours.Extensions;
using Mvp24Hours.Infrastructure.RabbitMQ.Core.Contract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Mvp24Hours.Infrastructure.RabbitMQ
{
    public abstract class MvpRabbitMQConsumer<T> : MvpRabbitMQBase, IMvpRabbitMQConsumer<T>
        where T : class
    {
        protected event EventHandler<Exception, BasicDeliverEventArgs> Failure;

        private EventingBasicConsumer _event;

        protected MvpRabbitMQConsumer()
            : base(typeof(T).Name)
        {
        }

        protected MvpRabbitMQConsumer(string routingKey)
            : base(routingKey)
        {
        }

        protected MvpRabbitMQConsumer(string hostAddress, string routingKey)
            : base(hostAddress, routingKey)
        {
        }

        protected MvpRabbitMQConsumer(RabbitMQConfiguration configuration, string routingKey)
            : base(configuration, routingKey)
        {
        }

        protected MvpRabbitMQConsumer(RabbitMQConfiguration configuration, RabbitMQQueueOptions options)
            : base(configuration, options)
        {
        }

        public virtual void Consume()
        {
            try
            {
                if (_event == null)
                {
                    _event = new EventingBasicConsumer(Channel);
                    _event.Received += EventReceived;

                    Channel.QueueBind(queue: Options.Queue ?? string.Empty,
                                            exchange: Options.Exchange,
                                            routingKey: Options.RoutingKey);
                }

                Channel.BasicConsume(queue: Options.Queue ?? string.Empty,
                     autoAck: Options.AutoAck,
                     consumer: _event);
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
                throw;
            }
        }

        private void EventReceived(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var body = e.Body.ToArray();
                string messageString = Encoding.UTF8.GetString(body);
                T message = messageString.ToDeserialize<T>();
                Received(message);
                if (!Options.AutoAck)
                {
                    Channel.BasicAck(e.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                if (!Options.AutoAck)
                {
                    Channel.BasicNack(e.DeliveryTag, false, true);
                }
                Logging.Error(ex);
                Failure?.Invoke(ex, e);
            }
        }

        public abstract void Received(T message);
    }
}
