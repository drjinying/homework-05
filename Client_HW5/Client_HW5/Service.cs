using System;
using System.Windows.Forms;
using System.IO;

namespace AsyncTcpServer
{
    public class Service
    {
        ListBox listbox;
        StreamWriter sw;       
        public Service(ListBox listbox, StreamWriter sw)
        {
            this.listbox = listbox;
            this.sw = sw;
        }
        public void SendToServer(string str)
        {
            try
            {
                sw.WriteLine(str);
                sw.Flush();
            }
            catch(Exception ex)
            {
                SetListBox("Send Faild: " + ex.Message);
            }
        }
        delegate void ListBoxCallback(string str);
        public void SetListBox(string str)
        {
            if (listbox.InvokeRequired == true)
            {
                ListBoxCallback d = new ListBoxCallback(SetListBox);
                listbox.Invoke(d, str);
            }
            else
            {
                listbox.Items.Add(str);
                listbox.SelectedIndex = listbox.Items.Count - 1;
                listbox.ClearSelected();
            }
        }
    }
}