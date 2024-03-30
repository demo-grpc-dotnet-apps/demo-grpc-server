# demo-grpc-server

gRPC Server Project

[![Build And Test](https://github.com/demo-grpc-dotnet-apps/demo-grpc-server/actions/workflows/build.yaml/badge.svg?branch=main)](https://github.com/demo-grpc-dotnet-apps/demo-grpc-server/actions/workflows/build.yaml)

## How to Build

```shell
dotnet build src/DemoComp.DemoGrpcServer/DemoComp.DemoGrpcServer.csproj
```

## How to Run

```shell
dotnet run --project src/DemoComp.DemoGrpcServer/DemoComp.DemoGrpcServer.csproj
```

## How to Use

Use [gRPCurl](https://github.com/fullstorydev/grpcurl) to test the server.

```shell
grpcurl -plaintext -d '{"name": "Taro"}' localhost:5231 greet.Greeter/SayHello
```

## How to Test

```shell
$ grpcurl -d '{"name": "Taro"}' localhost:7054 greet.Greeter/SayHello
```

### Expected Result

```json
{
  "message": "Hello Taro"
}
```

## How to Run XUnit 

```shell
dotnet test tests/DemoComp.DemoGrpcServer.Test/DemoComp.DemoGrpcServer.Test.csproj
```

### Coverage

```shell
dotnet test tests/DemoComp.DemoGrpcServer.Test/DemoComp.DemoGrpcServer.Test.csproj --collect:"XPlat Code Coverage" 
```

```shell
dotnet tool install -g dotnet-reportgenerator-globaltool
```

TestResults/ の後は任意に書き換えること

```shell
reportgenerator -reports:"tests/DemoComp.DemoGrpcServer.Test/TestResults/7cf69067-4704-4d46-8fbb-223df8cecfac/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

## How to Apply Code Formatter

```shell
dotnet format
```
