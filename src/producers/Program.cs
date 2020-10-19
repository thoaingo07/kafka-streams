using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace producers
{
    class Program
    {
        public static async Task Main()
        {
            string topic = "hello_kafka";
            Console.WriteLine("Hello Kafka Producers");
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            // Create a producer that can be used to send messages to kafka that have no key and a value of type string 
            using var p = new ProducerBuilder<Null, string>(config).Build();

            var i = 0;
            while (true)
            {
                // Construct the message to send (generic type must match what was used above when creating the producer)
                var message = new Message<Null, string>
                {
                    Value = $"{DateTime.Now.ToString("ddd hh:mm:ss")} Message #{++i}"
                };

                // Send the message to our test topic in Kafka                
                var dr = await p.ProduceAsync(topic, message);
                Console.WriteLine($"Produced message '{dr.Value}' to topic {dr.Topic}, partition {dr.Partition}, offset {dr.Offset}");

                Thread.Sleep(5000);
            }
        }
    }
}
