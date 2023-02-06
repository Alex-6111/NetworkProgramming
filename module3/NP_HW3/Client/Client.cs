using System;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new UdpClient();
            var serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            Console.WriteLine("Enter ingredients separated by commas: ");
            var ingredients = Console.ReadLine();
            var requestBytes = System.Text.Encoding.UTF8.GetBytes(ingredients);
            client.Send(requestBytes, requestBytes.Length, serverEndPoint);

            var responseBytes = client.Receive(ref serverEndPoint);
            var response = System.Text.Encoding.UTF8.GetString(responseBytes);
            Console.WriteLine(response.ToString());
        }
    }
}
