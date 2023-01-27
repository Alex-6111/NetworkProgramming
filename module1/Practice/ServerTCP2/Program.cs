using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class ServerTCP2
{
    static void Main(string[] args)
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8000);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        socket.Bind(endPoint);
        socket.Listen(10);

        while (true)
        {
            Socket client = socket.Accept();
            Task.Run(() => HandleClientAsync(client));
        }
    }

    static async Task HandleClientAsync(Socket client)
    {
        byte[] buffer = new byte[1024];
        int bytesReceived = await client.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
        string request = Encoding.ASCII.GetString(buffer, 0, bytesReceived);

        if (request == "date")
        {
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            byte[] data = Encoding.ASCII.GetBytes(date);
            await client.SendAsync(new ArraySegment<byte>(data), SocketFlags.None);
        }
        else if (request == "time")
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            byte[] data = Encoding.ASCII.GetBytes(time);
            await client.SendAsync(new ArraySegment<byte>(data), SocketFlags.None);
        }

        client.Shutdown(SocketShutdown.Both);
        client.Close();
    }
}
