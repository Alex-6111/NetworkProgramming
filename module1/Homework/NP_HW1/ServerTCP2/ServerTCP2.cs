using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerTCP2
{
    public partial class ServerTCP2 : Form
    {
        private IPEndPoint endPoint;
        private Socket socket;
        public ServerTCP2()
        {
            InitializeComponent();
            endPoint = new IPEndPoint(IPAddress.Any, 8000);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            socket.Bind(endPoint);
            socket.Listen(10);

            Task.Run(() => ListenForClientsAsync());
        }
        private async void ListenForClientsAsync()
        {
            while (true)
            {
                Socket client = socket.Accept();
                Task.Run(() => HandleClientAsync(client));
            }
        }

        private async void HandleClientAsync(Socket client)
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
}
