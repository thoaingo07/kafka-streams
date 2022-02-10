using Confluent.Kafka;
using System;
using System.Threading;

namespace consumers
{
    class Program
    {

        static void Main()
        {
            string topic = core.KafkaConsts.Topic;
            Console.Write("Enter your consumer group name:");
            var groupName = Console.ReadLine();            
            Console.WriteLine("Kafka consumer is running....");
            ConfluentConsume(topic, groupName);
        }



        private static void ConfluentConsume(string topic, string groupName)
        {
            var conf = new ConsumerConfig
            {
                GroupId = !string.IsNullOrWhiteSpace(groupName) ? groupName : core.KafkaConsts.ConsumerGroup1,
                BootstrapServers = core.KafkaConsts.BootstrapServers,
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<long, string>(conf).Build())
            {

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };
                consumer.Subscribe(topic);
                Console.WriteLine("Consuming messages from topic: " + topic);
                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = consumer.Consume(cts.Token);
                            Console.WriteLine($"[{core.KafkaConsts.ConsumerGroup1}] Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                            consumer.Commit(cr);

                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }


        }
    }
}
