using System;
using System.Net.Http;
using System.Threading.Tasks;
using GrpcGreeter;
using Grpc.Net.Client;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Client GRPC");

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var client = new Multiply.MultiplyClient(GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = new HttpClient(httpClientHandler) }));
            var reply = await client.ComputeMultiplyAsync(new RequestedNumbers { FirstNumber = 5, SecondNumber = 5});
            Console.WriteLine("Greeting: " + reply.Result);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
