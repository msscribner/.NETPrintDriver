using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

class Data 
{ 
    public string? appId { get; set; }
    public string? type { get; set; }
    public string? socketId { get; set; }
    public string? message { get; set; }
}


class Program
{
    public static string? SocketId { get; set; }
    public static string? AppId { get; set; }

    static async Task Main(string[] args)
    {
        // Create a WebSocket client
        ClientWebSocket webSocket = new ClientWebSocket();

        try
        {
            AppId = Guid.NewGuid().ToString();

            Console.WriteLine($"AppId is [{AppId}]");


            // Connect to the WebSocket server
            await webSocket.ConnectAsync(new Uri("ws://localhost:8080"), CancellationToken.None);
            Console.WriteLine("Connected to the WebSocket server.");


            // Get the WebSocket Id
            var data = await ReadWebSocket(webSocket);
            if (data != null) 
            {
                SocketId = data.socketId;
                Console.WriteLine($"WebSocketId is [{SocketId}]");
            }
            // Need to handle if data is NULL

            // Send a message to the server
            await SendMessageAsync(webSocket, AppId, "ASSOCIATEAPPWITHSOCKET", SocketId, "The .NET client has connected!");
            data = await ReadWebSocket(webSocket);
            if (data != null)
            {
            }
            //todo: Need to handle if data is NULL


            // Start Thread to send messages back to WebSocket Server
            Thread thread = new Thread(() => SendMessageThread(webSocket, AppId, SocketId, null));
            thread.Start();


            // Start a loop to continually read/send to/from the server
            while (webSocket.State == WebSocketState.Open)
            {
                data = await ReadWebSocket(webSocket);

                // Send a message to the server
                await SendMessageAsync(webSocket, AppId, "CLIENT-DOTNET", SocketId, "Hello from .NET client!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Close the WebSocket connection
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            Console.WriteLine("Connection closed.");
        }
    }


    /// <summary>
    /// ReadWebSocket - Reads data from a WebSocket
    /// </summary>
    /// <param name="webSocket"></param>
    /// <returns></returns>
    private static async Task<Data> ReadWebSocket(ClientWebSocket webSocket)
    {
        byte[] buffer = new byte[1024];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        if (result.MessageType == WebSocketMessageType.Close)
        {
            Console.WriteLine("Server closed the connection.");
            return null;
        }

        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
        var data = JsonSerializer.Deserialize<Data>(receivedMessage);
        Console.WriteLine($"Received from server: {receivedMessage}");
        Console.WriteLine($"Received data.appId: {data.appId}");
        Console.WriteLine($"Received data.type: {data.type}");
        Console.WriteLine($"Received data.socketId: {data.socketId}");
        Console.WriteLine($"Received data.message: {data.message}");

        return data;
    }

    /// <summary>
    /// SendMessageThread - Sends Messages back to the WebSocket server
    /// </summary>
    /// <param name="webSocket"></param>
    /// <param name="socketId"></param>
    /// <param name="message"></param>
    private static async void SendMessageThread(ClientWebSocket webSocket, string appId, string socketId, string message)
    {
        int iCount = 0;
        while (true) 
        {
            iCount++;

            message = $"The [SendMessageThread] was encountered ({iCount})";
            Console.WriteLine($"Sending message [{message}] from the the .NET Client ");

            // Send a message to the server
            await SendMessageAsync(webSocket, appId, "CLIENT-DOTNET", socketId, message); 

            Thread.Sleep(7000);
        }
    }

    /// <summary>
    /// SendMessageAsync - Asynchronous method to send a message to the WebSocket server
    /// </summary>
    /// <param name="webSocket"></param>
    /// <param name="clientId"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    private static async Task SendMessageAsync(ClientWebSocket webSocket, string appId, string type, string socketId, string message)
    {
        var sendData = new Data();
        sendData.appId = appId;
        sendData.type = type; 
        sendData.socketId = socketId;
        sendData.message = message;

        // Convert the object to JSON
        string jsonMessage = JsonSerializer.Serialize(sendData);

        // Convert the JSON to BYTES
        byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage);
        
        // Send the message to the WebSocket Server
        await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }
}
