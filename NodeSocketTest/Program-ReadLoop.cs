#if false
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // Define server IP and port
        IPAddress serverAddress = IPAddress.Parse("127.0.0.1");
        int serverPort = 8080;

        // Create a TCP client
        TcpClient client = new TcpClient();

        try
        {
            // Connect to the TCP server
            client.Connect(serverAddress, serverPort);
            Console.WriteLine("Connected to Node.js TCP server");

            // Get the network stream for reading and writing
            NetworkStream stream = client.GetStream();

            // Read data from the server
            byte[] buffer = new byte[256]; // Buffer for reading
            int bytesRead;

            var bDataAvailable = stream.DataAvailable;


            // Continuously listen for messages
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received from TCP server: [{receivedData}]");

                bDataAvailable = stream.DataAvailable;


                // Send a sendingData to the server
                string sendingData = "Hello, TCP Server! The .NET Console App received your message.";
                byte[] dataToSend = Encoding.UTF8.GetBytes(sendingData);
                stream.Write(dataToSend, 0, dataToSend.Length);
                Console.WriteLine($"Sending to TCP server: [{sendingData}]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error:", ex.Message);
        }
        finally
        {
            // Close the connection
            client.Close();
            Console.WriteLine("Connection closed");
        }
    }
}
#endif
