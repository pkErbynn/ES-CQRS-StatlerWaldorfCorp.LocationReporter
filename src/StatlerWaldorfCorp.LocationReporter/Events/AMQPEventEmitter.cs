using System;
using System.Text;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace StatlerWaldorfCorp.LocationReporter.Events
{
	public class AMQPEventEmitter: IEventEmitter
	{
        private readonly ILogger logger;
        private AMQPOptions rabbitOptions;
        private ConnectionFactory connectionFactory;

        public const string QUEUE_LOCATIONRECORDED = "memberlocationrecoreded";

        public AMQPEventEmitter(ILogger<AMQPEventEmitter> logger, IOptions<AMQPOptions> amqpOptions)
		{
            this.logger = logger;
            this.rabbitOptions = amqpOptions.Value;

            connectionFactory = new ConnectionFactory();
            connectionFactory.UserName = rabbitOptions.Username;
            connectionFactory.Password = rabbitOptions.Password;
            connectionFactory.VirtualHost = rabbitOptions.VirtualHost;
            connectionFactory.HostName = rabbitOptions.HostName;
            connectionFactory.Uri = rabbitOptions.Uri;

            logger.LogInformation($"AMQP Event Emitter configured with URI {rabbitOptions.Uri}");

		}

        public void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent)
        {
            using(IConnection conn = connectionFactory.CreateConnection())
            {
                using(IModel channel = conn.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QUEUE_LOCATIONRECORDED,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );
                    string jsonPayload = locationRecordedEvent.toJson();
                    var body = Encoding.UTF8.GetBytes(jsonPayload);
                    channel.BasicPublish(
                        exchange: String.Empty,
                        routingKey: QUEUE_LOCATIONRECORDED,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}

