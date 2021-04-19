using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceIntro
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task SayHelloStream(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            for (int index = 0; index < 10; index++)
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = $"Hello {request.Name} / {index}"
                });

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
