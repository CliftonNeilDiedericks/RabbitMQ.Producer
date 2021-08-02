using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ.Producer
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            channel.QueueDeclare("demo-queue",
               durable: true,
               exclusive: false,
               autoDelete: false,
               arguments: null
               );

            for (int i = 0; i <= 100; i++)
            {
                var message = new { name = "Producer", Message = $"Hello! Count:{i}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                QueueProducer.Publish(channel);

                channel.BasicPublish("", "demo-queue", null, body);
                Thread.Sleep(1000);
            }
        }
    }
}
