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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClientTCP2
{
    public partial class ClientTCP2 : Form
    {
        public ClientTCP2()
        {
            InitializeComponent();
        }

        private void ClientTCP2_Load(object sender, EventArgs e)
        {
           
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(endPoint);
            string request = textBox1.Text;
            byte[] data = Encoding.ASCII.GetBytes(request);
            await socket.SendAsync(new ArraySegment<byte>(data), SocketFlags.None);

            byte[] buffer = new byte[1024];
            int bytesReceived = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesReceived);

            textBox2.Text = "Response: " + response;

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
