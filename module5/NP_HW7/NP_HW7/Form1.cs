using System.Net;

namespace NP_HW7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                     
            using (WebClient client = new WebClient())
            {
                string text = client.DownloadString("http://www.gutenberg.org/files/1524/1524-0.txt");
                textBox1.Text = text;
            }
            
        }
    }

}
    
