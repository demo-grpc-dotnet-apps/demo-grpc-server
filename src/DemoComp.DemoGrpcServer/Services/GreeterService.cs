using Grpc.Core;
using Serilog;

namespace DemoComp.DemoGrpcServer.Services;

public class GreeterService : Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        Log.Information("Received gRPC request for SayHello. Name: {Name}", request.Name);

        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
