using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class ClientTCP2
{
    static async Task Main(string[] args)
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        socket.Connect(endPoint);

        Console.WriteLine("Enter 'date' or 'time':");
        string request = Console.ReadLine();
        byte[] data = Encoding.ASCII.GetBytes(request);
        await socket.SendAsync(new ArraySegment<byte>(data), SocketFlags.None);

        byte[] buffer = new byte[1024];
        int bytesReceived = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
        string response = Encoding.ASCII.GetString(buffer, 0, bytesReceived);

        Console.WriteLine("Response: " + response);

        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}
