using Grpc.Core;
using SampleGrpcServer;

namespace SampleGrpcServer.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        if (request.Name.IndexOf("5") >= 0)
        {
            var httpContext = context.GetHttpContext();
            httpContext.Connection.RequestClose();
            _logger.LogError("Restart client/server connection.");
            return Task.FromResult(default(HelloReply));
        }

        _logger.LogWarning($"Received request: {request.Name}");
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
