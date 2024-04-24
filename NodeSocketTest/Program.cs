#if false

using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // Create a TCP client to connect to the Node.js TCP server
        TcpClient client = new TcpClient();

        // Connect to the server
        client.Connect("127.0.0.1", 8080);

        // Create a stream to read data
        NetworkStream stream = client.GetStream();

        // Read data from the server
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);

        // Convert the byte data to a string
        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"Received from TCP server: {receivedMessage}");

        // Close the stream and client
        stream.Close();
        client.Close();
    }
}
#endif