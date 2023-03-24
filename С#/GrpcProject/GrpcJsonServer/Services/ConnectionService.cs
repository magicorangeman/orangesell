using Grpc.Core;

namespace GrpcJsonServer.Services
{
    public class ConnectionService : Connection.ConnectionBase
    {
        public override Task<ConnectionStatus> Connect(ConnectionRequest request, ServerCallContext context)
        {
            Console.WriteLine("Informing that connection success to " + request.Name);
            return Task.FromResult(new ConnectionStatus
            {
                Message = request.Name
            });
        }
    }
}