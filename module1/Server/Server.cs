
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ServerTCP
{
    internal class Server
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            const string ip_user = "127.0.0.1";
            const int port = 8000;
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(ip_user);
            IPEndPoint ep = new IPEndPoint(ip, port);
            s.Bind(ep);
            s.Listen(15);
            try
            {
                while (true)
                {
                    Socket ns = s.Accept();
                    Console.WriteLine($"[{ns.RemoteEndPoint.ToString()}]: Привіт Сервер!");
                    await ns.SendAsync(System.Text.Encoding.Unicode.GetBytes("Привіт клієнт!"));
                    Console.ReadKey();
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}