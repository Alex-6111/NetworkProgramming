using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NP_HW1
{
    public partial class ServerTCP : Form
    {
        private const string ip_user = "127.0.0.1";
        private const int port = 8000;
        private Socket s;
        public ServerTCP()
        {
            InitializeComponent();
        }

        private void ServerTCP_Load(object sender, EventArgs e)
        {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(ip_user);
            IPEndPoint ep = new IPEndPoint(ip, port);
            s.Bind(ep);
            s.Listen(15);
            Task.Run(() => ListenForClients());
        }

        private void ListenForClients()
        {
            try
            {
                while (true)
                {
                    Socket ns = s.Accept();
                    Invoke((MethodInvoker)delegate
                    {
                        textBox1.AppendText($"[{ns.RemoteEndPoint.ToString()}]: Привіт Сервер!\r\n");
                    });
                    ns.Send(Encoding.Unicode.GetBytes("Привіт клієнт!"));
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {
                Invoke((MethodInvoker)delegate
                {
                    textBox1.AppendText(ex.Message + "\r\n");
                });
            }
        }

        private void ServerTCP_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (s != null)
            {
                s.Close();
            }
        }
    }
}
