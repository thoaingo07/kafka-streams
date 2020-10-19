using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace consumers
{
    class Program
    {

        static void Main()
        {
            string topic = "hello_kafka";

            ConfluentConsume(topic);
        }



        private static void ConfluentConsume(string topic)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer",
                BootstrapServers = "localhost:9092"
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                var topicPartitionOffset = new TopicPartitionOffset(topic, new Partition(0), new Confluent.Kafka.Offset(0));

                c.Assign(topicPartitionOffset);
                //c.Assign(topic, 0, new Offset(200));
                //c.Subscribe(topic);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };
                try
                {
                    while (!cts.IsCancellationRequested)
                    {
                        var cr = c.Consume(TimeSpan.FromMilliseconds(50));
                        if (cr != null )
                        {
                            Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");

                        }

                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    
                }
                finally
                {
                    c.Close();
                }
            }
        }
    }
}
