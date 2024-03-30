using System.Net;
using FluentAssertions;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit.Abstractions;

namespace DemoComp.DemoGrpcServer.Test.IntegrationTests.Greet;

public class SayHelloTest : IClassFixture<WebApplicationFactory<Program>>
{
    /// <summary>
    ///     gRPC Endpoint.
    /// </summary>
    private const string Uri = "https://localhost:7054";

    /// <summary>
    ///     gRPC Client.
    /// </summary>
    private readonly Greeter.GreeterClient _client;

    /// <summary>
    ///     Represents a class which can be used to provide test output.
    /// </summary>
    private readonly ITestOutputHelper _testOutputHelper;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="webApplicationFactory">Factory for bootstrapping an application in memory for functional end to end tests.</param>
    /// <param name="testOutputHelper">Logger</param>
    public SayHelloTest(WebApplicationFactory<Program> webApplicationFactory, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        var client = webApplicationFactory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                // ConfigureServices of DI
            });
        }).CreateClient();
        client.DefaultRequestVersion = HttpVersion.Version20;

        var channel = GrpcChannel.ForAddress(Uri, new GrpcChannelOptions
        {
            HttpClient = client
        });

        _client = new Greeter.GreeterClient(channel);
    }

    [Theory]
    [InlineData("Taro", "Hello Taro")]
    public async Task Test(string inputName, string expectedMessage)
    {
        // -------------------
        // Setup
        // -------------------
        var request = new HelloRequest { Name = inputName };

        // -------------------
        // Exercise
        // -------------------
        var reply = await _client.SayHelloAsync(request);

        // -------------------
        // Verify
        // -------------------
        reply.Message.Should().Be(expectedMessage);

        // Logging
        _testOutputHelper.WriteLine($"Greeting: {reply.Message}");
    }
}
