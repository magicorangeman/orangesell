using Grpc.Core;

namespace GrpcJsonServer.Services
{
    public class JsonListSenderService : JsonListSender.JsonListSenderBase
    {
        public override async Task<JsonListUploadResponse> Upload(IAsyncStreamReader<JsonListUploadRequest> request, ServerCallContext context)
        {
            Console.WriteLine($"Getting list of json types...");
            while (await request.MoveNext())
            {
                var list = request.Current.List;
                Console.WriteLine($"{list.Type} : {list.Number}");
            }

            Console.WriteLine("Specify json type for download");
            string? type = Console.ReadLine();

            return new JsonListUploadResponse { Type = type };
        }
    }
}