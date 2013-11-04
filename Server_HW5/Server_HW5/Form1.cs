using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Threading;

namespace Server_HW5
{


    public partial class Form1 : Form
    {
        public TcpClient client;
        public StreamReader sr;
        public StreamWriter sw;
        public string str;

        IPAddress localAddress;
        private int port = 51888;
        private TcpListener mylistener;
        TcpClient myClient;
        NetworkStream netStream;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stript = textBox2.Text.Trim();
            send(stript);           
        }


        private void send(string str)
        {
            sw.WriteLine(str);
            sw.Flush();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            IPAddress[] hostip = Dns.GetHostAddresses(Dns.GetHostName());
            localAddress = hostip[2];
            //localAddress = IPAddress.Parse("127.0.0.1");
            mylistener = new TcpListener(localAddress, port);
            mylistener.Start();
            Thread myThread = new Thread(new ThreadStart(ListenClientConnect));
         
            myThread.Start();
            button1.Enabled = true;
        }
        private void ListenClientConnect()
        {
            while (true)
            {
                TcpClient newClient = null;
                textBox1.AppendText("listening; ");
                textBox1.AppendText(localAddress.ToString());
                textBox1.AppendText(port.ToString());
                try
                {
                    newClient = mylistener.AcceptTcpClient();
                }
                catch (System.Exception ex)
                {
                    break;
                }
                myClient = newClient;
                netStream = myClient.GetStream();
                sr = new StreamReader(netStream, System.Text.Encoding.UTF8);
                sw = new StreamWriter(netStream, System.Text.Encoding.UTF8);
                textBox1.AppendText("connected\n/n");
                Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
                threadReceive.Start();
            }

        }
        private void ReceiveData()
        {
            TcpClient client = myClient;
            while (true)
            {
                string rcv = null;
                try
                {
                    rcv = sr.ReadLine();
                    textBox1.AppendText(rcv + "/n");
                }
                catch (System.Exception ex)
                {
                    textBox1.AppendText("receving err");
                    break;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myClient.Close();
            mylistener.Stop();
        }
        
    }
}
