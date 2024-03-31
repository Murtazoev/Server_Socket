using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server_Socket
{
    internal class Class1
    {
        static Socket client;
        static IPEndPoint ipEND = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
        static void Main(string[] args)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(ipEND);
            }
            catch
            {
                Console.WriteLine("Unable to connect to server !");
            }
            string mess;
            mess = Console.ReadLine();
            byte[] buffer = new byte[1024];
            buffer = Encoding.UTF8.GetBytes(mess);
            client.Send(buffer);
            Console.WriteLine("Message was send !");
            Console.Read();
        }
    }
}
