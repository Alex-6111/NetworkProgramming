using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace NP_HW6
{
    public partial class Form1 : Form
    {
        private readonly int _port = 8000;
        private readonly UdpClient _client = new UdpClient();
        private IPEndPoint _remoteIpEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            byte[] sendBytes = System.Text.Encoding.ASCII.GetBytes(message);

            _client.Send(sendBytes, sendBytes.Length, _remoteIpEndPoint);
            txtMessage.Text = "";
            txtChat.Text += "You: " + message + Environment.NewLine;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _client.Connect(_remoteIpEndPoint);
            _client.BeginReceive(DataReceived, null);
        }

        private void DataReceived(IAsyncResult result)
        {
            IPEndPoint receiveIpEndPoint = new IPEndPoint(IPAddress.Any, _port);
            byte[] receivedBytes = _client.EndReceive(result, ref receiveIpEndPoint);
            string message = System.Text.Encoding.ASCII.GetString(receivedBytes);

            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txtChat.Text += receiveIpEndPoint.Address.ToString() + ": " + message + Environment.NewLine;
                });
            }
            else
            {
                txtChat.Text += receiveIpEndPoint.Address.ToString() + ": " + message + Environment.NewLine;
            }

            _client.BeginReceive(DataReceived, null);
        }
    }
}

