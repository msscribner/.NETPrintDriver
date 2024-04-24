#if false
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // Define the server's IP address and port
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // Localhost
        int port = 3001; // Port to listen on

        // Create a TcpListener to listen for incoming connections
        TcpListener server = new TcpListener(ipAddress, port);

        try
        {
            // Start the TCP listener
            server.Start();
            Console.WriteLine($"TCP Server listening on {ipAddress}:{port}");

            while (true)
            {
                // Wait for an incoming connection
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected");

                // Get the network stream for reading and writing
                NetworkStream stream = client.GetStream();

                // Read data from the client
                byte[] buffer = new byte[256]; // Buffer for reading
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Received from client: {receivedData}");

                // Optionally, respond to the client
                string responseMessage = "Data received!";
                byte[] responseData = Encoding.ASCII.GetBytes(responseMessage);
                stream.Write(responseData, 0, responseData.Length);

                // Close the client connection
                client.Close();
                Console.WriteLine("Client disconnected");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Stop the server when exiting
            server.Stop();
        }
    }
}
#endif