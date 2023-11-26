using Amazon.SQS;
using Amazon.SQS.Model;

var cancellationTokenSource = new CancellationTokenSource();
var sqsClient = new AmazonSQSClient();

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var receiveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    AttributeNames = ["All"],
    MessageAttributeNames = ["All"]
};

while (!cancellationTokenSource.IsCancellationRequested)
{
    var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, cancellationTokenSource.Token);

    foreach (var message in response.Messages)
    {
        Console.WriteLine($"MessageId: {message.MessageId}");
        Console.WriteLine($"MessageBody: {message.Body}");

        await sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle, cancellationTokenSource.Token);
    }

    await Task.Delay(3000);
}