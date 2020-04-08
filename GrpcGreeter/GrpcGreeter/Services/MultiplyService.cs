using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcGreeter
{
    public class MultiplyService : Multiply.MultiplyBase
    {
        private readonly ILogger<MultiplyService> _logger;
        public MultiplyService(ILogger<MultiplyService> logger)
        {
            _logger = logger;
        }
        public override Task<MultiplyResult> ComputeMultiply(RequestedNumbers request, ServerCallContext context)
        {
            int first = request.FirstNumber;
            int second = request.SecondNumber;

            return Task.FromResult(new MultiplyResult
            {
                Result = Mult(first, second)
            });
        }

        private static int Mult(int first, int second)
        {
            return first * second;
        }
    }
}
