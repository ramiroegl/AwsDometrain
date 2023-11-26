using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Customers.Consumer.Messaging;
using MediatR;
using Microsoft.Extensions.Options;

namespace Customers.Consumer;

public class QueueConsumerWorker(IAmazonSQS sqs, IMediator mediator, IOptions<QueueSettings> options, ILogger<QueueConsumerWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrlResponse = await sqs.GetQueueUrlAsync(options.Value.QueueName, stoppingToken);

        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
            AttributeNames = ["All"],
            MessageAttributeNames = ["All"],
            MaxNumberOfMessages = 1
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await sqs.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);
            foreach (var message in response.Messages)
            {
                var messageType = message.MessageAttributes["MessageType"].StringValue;

                var type = Type.GetType($"Customers.Consumer.Messaging.{messageType}");

                if (type is null)
                {
                    logger.LogWarning("Unknown message type {MessageType}", messageType);
                    continue;
                }

                var typedMessage = (ISqsMessage)JsonSerializer.Deserialize(message.Body, type)!;
                try
                {
                    await mediator.Send(typedMessage, stoppingToken);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Message failed while processing");
                    continue;
                }

                await sqs.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle, stoppingToken);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}