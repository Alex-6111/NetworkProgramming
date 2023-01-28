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
 

namespace ClientTCP
{
    public partial class ClientTCP : Form
    {
        private readonly string _ip = "127.0.0.1";
        private readonly int _port = 8000;
        public ClientTCP()
        {
            InitializeComponent();
        }

        private void ClientTCP_Load(object sender, EventArgs e)
        {

        }

        private async void ConnectToServer()
        {
            IPAddress ip = IPAddress.Parse(_ip);
            IPEndPoint ep = new IPEndPoint(ip, _port);
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    await s.ConnectAsync(ep);
                    if (s.Connected)
                    {
                        string strSend = "GET\r\n\r\n";
                        s.Send(Encoding.Unicode.GetBytes(strSend));
                        byte[] buffer = new byte[1024];
                        int l;
                        do
                        {
                            l = s.Receive(buffer);
                            textBox1.AppendText(Encoding.Unicode.GetString(buffer, 0, l));
                        } while (l > 0);
                    }
                    else
                    {
                        MessageBox.Show("Error connecting to the server");
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            ConnectToServer();
        }
    }
}
