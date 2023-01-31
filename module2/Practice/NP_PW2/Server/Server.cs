using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Server
    {
        private static List<string> quotes = new List<string> { "\"The only way to do great work is to love what you do.\" - Steve Jobs",
                                                                    "\"Don't let yesterday take up too much of today.\" - Will Rogers",
                                                                    "\"Be the change you wish to see in the world.\" - Mahatma Gandhi",
                                                                    "\"Believe in yourself and all that you are. Know that there is something inside you that is greater than any obstacle.\" - Christian D. Larson",
                                                                    "\"It does not matter how slowly you go as long as you do not stop.\" - Confucius"};
        private static List<Tuple<string, DateTime>> log = new List<Tuple<string, DateTime>>();
        private static TcpListener listener;
        static void Main(string[] args)
        {
            listener = new TcpListener(IPAddress.Any, 8000);
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread thread = new Thread(HandleClient);
                thread.Start(client);
            }
        }

        private static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();

            string clientName = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            log.Add(Tuple.Create(clientName, DateTime.Now));
            Console.WriteLine(clientName + " connected at " + DateTime.Now);

            while (true)
            {
                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    log.Add(Tuple.Create(clientName, DateTime.Now));
                    Console.WriteLine(clientName + " disconnected at " + DateTime.Now);
                    break;
                }

                string request = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
                if (request == "get quote")
                {
                    Random rnd = new Random();
                    int quoteIndex = rnd.Next(quotes.Count);
                    string quote = quotes[quoteIndex];

                    byte[] quoteBuffer = System.Text.Encoding.ASCII.GetBytes(quote);
                    stream.Write(quoteBuffer, 0, quoteBuffer.Length);
                }

            }


        }

    }
}