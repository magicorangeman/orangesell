using Google.Protobuf;
using Grpc.Net.Client;
using JsonClient;
using MoreLinq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Configuration;
using System.Collections.Specialized;

NameValueCollection config;
config = ConfigurationManager.AppSettings;
string path = config.Get("path");
int batchSize = int.Parse(config.Get("batchSize"));
string server = config.Get("server");
var date = DateTime.ParseExact(config.Get("date"), "dd.MM.yyyy", default);
var user = config.Get("user");


using var channel = GrpcChannel.ForAddress(server);
var client = new Connection.ConnectionClient(channel);
var connectReply = await client.ConnectAsync(new ConnectionRequest { Name = user });
Console.WriteLine(connectReply.Message + " is connected");
Console.WriteLine("Press any key to get json lists ...");
Console.ReadKey();

// getting files from directory
var files = FileManager.GetFilesForSend(path, date);
var jsonLists = FileManager.GetJsonTypes(files);

var information = new JsonListSender.JsonListSenderClient(channel);
using var jsonSendRequest = information.Upload();
foreach (var jsonList in jsonLists)
{
    await jsonSendRequest.RequestStream.WriteAsync(
        new JsonListUploadRequest { List = jsonList });
}
await jsonSendRequest.RequestStream.CompleteAsync();
Console.WriteLine("Waiting response from server ...");
var response = await jsonSendRequest.ResponseAsync;
var types = response.Type; //get a requested type of json files

var sender = new Sender.SenderClient(channel);

if (types != "all")
{
    var jsonList = jsonLists
        .Where(x => x.Type == types);
}

foreach (var jsonList in jsonLists)
{
    using var fileUploadRequest = sender.Upload();
    var jsonArray = new JsonArray(FileManager.GetJsonArray(jsonList.Type, files));
    foreach (var batch in jsonArray.Batch(batchSize))
    {
        var bytes = ByteString.CopyFrom(JsonSerializer.SerializeToUtf8Bytes(batch));
        await fileUploadRequest.RequestStream.WriteAsync(
            new FileUploadRequest { Type = jsonList.Type, Content = bytes });
    }
    await fileUploadRequest.RequestStream.CompleteAsync();
    var result = await fileUploadRequest.ResponseAsync;
    Console.WriteLine($"Result upload: Type = {result.Type} : {result.Status}");
}

Console.ReadLine();

if (types == "all")
    foreach(var file in files)
        File.Delete(file.FullName);


    