using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace _200408_Hexapod.Communication
{
    class TcpIp
    {
        TcpListener Listener = null;
        TcpClient Client     = null;
        NetworkStream stream = null;

        byte[] buff = new byte[1024];
        public TcpIp(string strIPAddress)
        {
            IPEndPoint Address = new IPEndPoint(IPAddress.Parse(strIPAddress),5000);
            Listener = new TcpListener(Address);

        }
        
        public void ListenerStart()
        {
            Listener.Start();
        }

        public void AcceptTCPClient()
        {
            try
            {
                Client = Listener.AcceptTcpClient();
                stream = Client.GetStream();

                ReadDataStreamForClient();
            }
            catch
            {

            }
          
        }
        public void ReadDataStreamForClient()
        {
            int length;
            string data = null;

            while((length = stream.Read(buff, 0, buff.Length)) != 0)
            {
                data = Encoding.Default.GetString(buff, 0, length);
                Console.WriteLine(string.Format("수신 : {0}", data));

                
            }
        }
    }
}
