using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace AsyncTcpServer
{
    public partial class mainForm : Form
    {

        const int STATE_REGISTER = 1;
        const int STATE_REGISTER_OK = 2;
        const int STATE_NEW_ROUND = 3;
        const int STATE_SUBMIT = 4;
        const int STATE_SUBMIT_OK = 5;
        const int STATE_SERVER_STOP = 6;
        const int INTERVAL = 3000;

        const string TYPE_REGISTER = "1";
        const string TYPE_REGISTER_OK = "2";
        const string TYPE_NEW_ROUND = "3";
        const string TYPE_SUBMIT = "4";
        const string TYPE_SUBMIT_OK = "5";
        const string TYPE_SERVER_STOP = "6";

        const string    DEFAULT_SERVER_IP = "192.168.1.2";    
        const int       DEFAULT_SERVER_PORT =  51888;
        const string    DEFAULT_USER_ID = "11061128";
        const string    DEFAULT_USER_PASSWD = "123456";

        private string ip_input = DEFAULT_SERVER_IP;
        private int port = DEFAULT_SERVER_PORT;
        private string id = DEFAULT_USER_ID;
        private string passwd = DEFAULT_USER_PASSWD;

        private int tstate = 2;
        private int State = STATE_REGISTER;
        private int Round = 0;
        private int[] PrevRslt = new int[5000];
        private int PrevRslt_idx = 0;
        private int GoldPoint;
        private string[] rcvs;
        private bool isRunning = false;

        private TcpClient client = null;
        private StreamWriter sw;
        private StreamReader sr;
        private Service service;
        private NetworkStream netStream;

        Thread threadRegister;
        Thread threadSubmit;
        Thread threadReceive;
        Thread threadWaitNewRound;
        Thread threadControl;

        public mainForm()
        {
            InitializeComponent();
            service = new Service(lb_log, sw);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (isRunning == true)
            {
                service.SetListBox("Already running, Press STOP first");
                return;
            }

            if (String.Compare(tb_svr_IP.Text, "IP") != 0)
            {
                ip_input = tb_svr_IP.Text.Trim();
            }
            if (String.Compare(tb_svr_port.Text, "PORT") != 0)
            {
                port = Int32.Parse(tb_svr_port.Text.Trim());
            }
            if (String.Compare(tb_id.Text, "ID") != 0)
            {
                id = tb_id.Text.Trim();
            }
            if (String.Compare(tb_passwd.Text, "PASSWORD") != 0)
            {
                passwd = tb_passwd.Text.Trim();
            }
            IPAddress serverIP = IPAddress.Parse(ip_input);
            client = new TcpClient();
            try
            {
                client.Connect(serverIP, port);
            }
            catch (System.Exception ex)
            {
                service.SetListBox(ex.Message);
                return;
            }
            try
            {
                netStream = client.GetStream();
            }
            catch (System.Exception ex)
            {
                service.SetListBox(ex.Message);
                return;
            }
            sr = new StreamReader(netStream, System.Text.Encoding.UTF8);
            sw = new StreamWriter(netStream, System.Text.Encoding.UTF8);
            service = new Service(lb_log, sw);
            isRunning = true;
            threadControl = new Thread(new ThreadStart(control));
            threadControl.Start();
        }

        private void control()
        {
            threadRegister = new Thread(new ThreadStart(Register));
            //threadSubmit = new Thread(new ThreadStart(Cal_and_Submit));
            threadReceive = new Thread(new ThreadStart(ReceiveData));
            //threadWaitNewRound = new Thread(new ThreadStart(WaitNewRound));
            //threadWaitNewRound.Start(); threadReceive.Suspend();
            //threadSubmit.Start(); threadSubmit.Suspend();

            while (isRunning == true)
            {
                if (tstate == 1) //wait finish
                {
                    threadWaitNewRound.Abort();
                    tstate = 0;
                }
                else if(tstate == 0)//waiting
                {
                    continue;
                }
                //nothing to wait
                switch (State)
                {
                    case STATE_REGISTER:
                        threadRegister.Start();
                        threadRegister.Join();
                        State = STATE_REGISTER_OK;
                        break;
                    case STATE_REGISTER_OK:
                        threadWaitNewRound = new Thread(new ThreadStart(WaitNewRound));
                        threadWaitNewRound.Start();
                        threadWaitNewRound.Join();
                        tstate = 2;
                        State = STATE_NEW_ROUND;/////
                        break;
                    case STATE_NEW_ROUND:
                        threadSubmit = new Thread(new ThreadStart(Cal_and_Submit));
                        threadSubmit.Start();
                        threadSubmit.Join();
                        State = STATE_SUBMIT_OK;
                        break;
                    case STATE_SUBMIT_OK:
                        threadWaitNewRound = new Thread(new ThreadStart(WaitNewRound));
                        threadWaitNewRound.Start();
                        threadWaitNewRound.Join();
                        State = STATE_NEW_ROUND;
                        tstate = 2;
                        break;
                    case STATE_SERVER_STOP:
                        service.SetListBox("Server Stop");
                        endMission();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ReceiveData()
        {
            while (isRunning == true)
            {
                string receiveString = null;
                try
                {
                    receiveString = sr.ReadLine();
                }
                catch (Exception e)
                {
                    service.SetListBox(e.Message);
                }

                if (receiveString == null)
                {
                    //
                    service.SetListBox("wait to re receive");
                    //
                    Thread.Sleep(INTERVAL);
                    continue;
                }
                //
                service.SetListBox("recieved" + receiveString);
                //
                rcvs = receiveString.Split(';');
                switch (rcvs[0])
                {
                    case TYPE_REGISTER_OK:
                        State = STATE_REGISTER_OK;
                        break;
                    case TYPE_SUBMIT_OK:
                        if (Round == Int32.Parse(rcvs[1]))
                        {
                            State = STATE_SUBMIT_OK;
                        }
                        break;
                    case TYPE_NEW_ROUND:
                        if (Round == Int32.Parse(rcvs[1]) - 1)
                        {
                            State = STATE_NEW_ROUND;
                        }
                        break;
                    case TYPE_SERVER_STOP:
                        State = STATE_SERVER_STOP;
                        service.SetListBox("Server Stop");
                        tstate = 2;
                        endMission();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Register()
        {
            threadReceive.Start();
            String str = TYPE_REGISTER + ";" + id + ";" + passwd;
            service.SendToServer(str);
            while (isRunning && State != STATE_REGISTER_OK)
            {
                //
                service.SetListBox("re register");
                //
                Thread.Sleep(INTERVAL);
                service.SendToServer(str);
            }
            threadReceive.Suspend();
        }

        private void WaitNewRound()
        {
            threadReceive.Resume();
            while (isRunning && State != STATE_NEW_ROUND)
            {
                //
                service.SetListBox("waiting");
                //
                Thread.Sleep(INTERVAL);
            }
            Round++;
            PrevRslt[PrevRslt_idx++] = Int32.Parse(rcvs[1]);
            threadReceive.Suspend();
            tstate = 1;
        }

        private void Cal_and_Submit()
        {
            threadReceive.Resume();
            GoldPoint = calculate();
            string str = TYPE_SUBMIT + ";" + Round.ToString() + ";" + GoldPoint.ToString();
            service.SendToServer(str);
            while (isRunning && State != STATE_SUBMIT_OK)
            {
                Thread.Sleep(INTERVAL);
                //
                service.SetListBox("submitting");
                //
                service.SendToServer(str);
            }
            threadReceive.Suspend();
        }
        private int calculate()
        {
            //
            service.SetListBox("calculation");
            //
            if (Round == 1)
            {
                return 100;
            }
            else
            return (int)(PrevRslt[PrevRslt_idx] * 0.618);
        }
 
        private void btn_stop_Click(object sender, EventArgs e)
        {
            endMission();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            endMission();
        }

        private void endMission()
        {
            //
            service.SetListBox("ending mission");
            //
            if (isRunning)
            {
                netStream.Close();
                client.Close();
            }
            isRunning = false;
        }
    }
}
