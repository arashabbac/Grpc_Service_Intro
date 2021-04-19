using Grpc.Core;
using System;

namespace ConsoleApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var channel = Grpc.Net.Client.GrpcChannel.ForAddress("https://localhost:5001");
            var client = new GrpcServiceIntro.Greeter.GreeterClient(channel);

            //var response = await client.SayHelloAsync(new GrpcServiceIntro.HelloRequest
            //{
            //    Name = ".Net Conf"
            //});

            //Console.WriteLine($"From Server : {response.Message}");

            var call = client.SayHelloStream(new GrpcServiceIntro.HelloRequest
            {
                Name = $".Net Conf"
            });

            await foreach (var item in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"From server streaming { item.Message }");
            }
        }
    }
}
