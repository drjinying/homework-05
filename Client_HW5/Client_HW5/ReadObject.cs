using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace AsyncTcpServer
{
    public class ReadObject
    {
        public NetworkStream netStream;
        public byte[] bytes;
     
        public ReadObject(NetworkStream netStream, int bufferSize)
        {
            this.netStream = netStream;
            bytes = new byte[bufferSize];
        }
    }
}
