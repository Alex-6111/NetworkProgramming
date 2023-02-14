using System;
using System.Net;


namespace NP_HW8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string text;
            using (var client = new HttpClient())
            {
                text =  await client.GetStringAsync("http://www.gutenberg.org/files/1524/1524-0.txt");
            }
            textBox1.Text = text;
            
            

        }
    }
}