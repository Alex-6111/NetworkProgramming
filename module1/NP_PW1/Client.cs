
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ClientTCP
{
    internal class Client
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            const string ip_user = "127.0.0.1";
            const int port = 8000;
            IPAddress ip = IPAddress.Parse(ip_user);
            IPEndPoint ep = new IPEndPoint(ip, port);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
               await s.ConnectAsync(ep);
                if (s.Connected)
                {
                    String strSend = "GET\r\n\r\n";
                    await s.SendAsync(System.Text.Encoding.Unicode.GetBytes(strSend));
                    byte[] buffer = new byte[1024];
                    int l;
                    do
                    {
                        l = s.Receive(buffer);
                        Console.WriteLine(System.Text.Encoding.Unicode.GetString(buffer, 0, l));
                    } while (l > 0);
                }
                else Console.WriteLine("Error");
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                s.Shutdown(SocketShutdown.Both);
                s.Close();
            }
        }
    }
}