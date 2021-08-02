using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class DirectExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            //message time to live
            var ttl = new Dictionary<string, object> { { "x-message-ttl", 30000 } };
           
            channel.ExchangeDeclare("demo-direct-exchange",
               ExchangeType.Direct,
               arguments:ttl
               );

            for (int i = 0; i <= 100; i++)
            {
                var message = new { name = "Producer", Message = $"Hello! Count:{i}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));             

                channel.BasicPublish("demo-direct-exchange", "account.init", null, body);
                Thread.Sleep(1000);
            }
        }
    }
}
