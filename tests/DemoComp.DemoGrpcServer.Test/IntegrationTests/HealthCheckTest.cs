using System.Net;
using FluentAssertions;
using Grpc.Health.V1;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Serilog;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DemoComp.DemoGrpcServer.Test.IntegrationTests;

public class HealthCheckTest : IClassFixture<WebApplicationFactory<Program>>
{
    /// <summary>
    ///     gRPC Endpoint.
    /// </summary>
    private const string Uri = "https://localhost:7054";

    /// <summary>
    ///     gRPC Client.
    /// </summary>
    private readonly Health.HealthClient _client;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="webApplicationFactory">Factory for bootstrapping an application in memory for functional end to end tests.</param>
    /// <param name="iTestOutputHelper">Logger</param>
    public HealthCheckTest(WebApplicationFactory<Program> webApplicationFactory, ITestOutputHelper iTestOutputHelper)
    {
        // gRPC Client
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
        _client = new Health.HealthClient(channel);

        // Logging
        var logOutput = (TestOutputHelper)iTestOutputHelper;
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.TestOutput(logOutput)
            .CreateLogger();
    }

    [Fact]
    public async Task Test()
    {
        // -------------------
        // Setup
        // -------------------
        var request = new HealthCheckRequest();

        // -------------------
        // Exercise
        // -------------------
        var reply = await _client.CheckAsync(request);

        // -------------------
        // Verify
        // -------------------
        reply.Status.Should().Be(HealthCheckResponse.Types.ServingStatus.Serving);
    }
}
