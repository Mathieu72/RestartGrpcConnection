using Grpc.Net.Client;
using SampleGrpcServer;

namespace SampleGrpcClient;

static class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var uriBuilder = new UriBuilder
        {
            Host = "localhost",
            Port = 5149
        };

        var channel = GrpcChannel.ForAddress(uriBuilder.Uri);

        var client = new Greeter.GreeterClient(channel);

        var count = 10;

        for (int i = 0; i < count; i++)
        {
            var request = new HelloRequest
            {
                Name = $"Mat number: {i}"
            };
            Console.WriteLine($"Request: {request.Name}");
            var response = await client.SayHelloAsync(request);
            if (response == null)
            {
                Console.WriteLine("The response is null");
            }
            else
            {
                Console.WriteLine($"Response: {response.Message}");
            }
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
    }
}
