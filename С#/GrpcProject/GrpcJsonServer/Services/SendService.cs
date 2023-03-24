using Google.Protobuf;
using Grpc.Core;
using System.Text;
using System.Text.Json;

namespace GrpcJsonServer.Services
{
    public class SenderService : Sender.SenderBase
    {
        public override async Task<FileUploadResponse> Upload(IAsyncStreamReader<FileUploadRequest> request, ServerCallContext context)
        {
            Console.WriteLine($"Downloading files from client...");
            var data = new List<ByteString>();
            var type = "";
            while (await request.MoveNext())
            {
                Console.WriteLine("Downloading status: in progress");
                data.Add(request.Current.Content);
                type = request.Current.Type;
            }
            var jsonBytes = data
                .Select(byteString => byteString.ToByteArray())
                .SelectMany(bytes => bytes)
                .ToArray();

            Directory.CreateDirectory($@"\root\{type}");
            File.WriteAllBytes($@"\root\{type}\{DateTime.Now:yyyy-MM-dd}.json", jsonBytes);

            return new FileUploadResponse { Type = type, Status = "Complete" };
        }
    }
}