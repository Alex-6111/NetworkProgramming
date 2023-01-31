using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

namespace Client_2
{
    internal class Client
    {
        static  void Main(string[] args)
        {
           TcpClient client = new TcpClient("127.0.0.1", 8000);
        NetworkStream stream = client.GetStream();

            while (true)
            {
                Console.WriteLine("Enter 'get quote' to get a quote or 'exit' to exit");
                string request = Console.ReadLine();

                if (request == "exit")
                {
                    break;
                }

                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(request);
                stream.Write(buffer, 0, buffer.Length);

                if (request == "get quote")
                {
                    buffer = new byte[256];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string quote = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received quote: " + quote);
                }
            }
        }
    }
}