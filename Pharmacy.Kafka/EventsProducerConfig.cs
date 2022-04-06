namespace Pharmacy.Kafka
{
    public record EventsProducerConfig
    {
        public EventsProducerConfig(string kafkaConnectionString, string topicBaseName)
        {
            KafkaConnectionString = kafkaConnectionString;
            TopicName = topicBaseName;
        }

        public string KafkaConnectionString { get; }
        public string TopicName { get; }
    }
}