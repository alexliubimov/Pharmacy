using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Kafka
{
    public record EventsConsumerConfig
    {
        public EventsConsumerConfig(string kafkaConnectionString, string topicBaseName, string consumerGroup)
        {
            KafkaConnectionString = kafkaConnectionString;
            TopicName = topicBaseName;
            ConsumerGroup = consumerGroup;
        }

        public string KafkaConnectionString { get; }
        public string TopicName { get; }
        public string ConsumerGroup { get; }
    }
}
