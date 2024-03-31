using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Timers;
using System.Runtime.Intrinsics.X86;

namespace chat
{
    public class Server
    {
        static System.Timers.Timer timer = new System.Timers.Timer();
        static Socket srvr;
        static Socket x, y;
        static IPEndPoint ipEND = new IPEndPoint(0, 1234);
        static byte[] empty = new byte[1024];
        static int cnt = 0;
        static void Main(string[] args)
        {
            Console.Title = "Server";
            srvr = new (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            srvr.Bind(ipEND);
            Console.WriteLine("Waiting for clients to connect !");
            srvr.Listen(1000);
            x = srvr.Accept();
            y = srvr.Accept();
            timer.Elapsed += TimerEventProcessor;
            SetTimer();
            while (true) { }
            srvr.Close();
        }
        private static void SetTimer()
        {
            timer.Elapsed += TimerEventProcessor;
            timer.Interval = 1000;
            timer.Start();
        }

        private static void TimerEventProcessor(object myObject , EventArgs myEventsArgs)
        {
            ReceiveMessage();
            ReceiveFromClient2();
        }
        public static async void ReceiveMessage()
        {
            byte[] buffer = new byte[1024];
            await x.ReceiveAsync(buffer);
            string message;
            message = Encoding.UTF8.GetString(buffer);
            if (message != null)
            {
                x.Send(buffer);
                y.Send(buffer);
                Console.WriteLine("Client 1 : " +  message);
            }
        }

        public async static void ReceiveFromClient2()
        {
            byte[] buffer = new byte[1024];
            await y.ReceiveAsync(buffer);
            string message = Encoding.UTF8.GetString(buffer);
            if (message != null)
            {
                x.Send(buffer);
                y.Send(buffer);
                Console.WriteLine("Client 2 : " + message);
            }
        }
    }
}










/*
            static Socket srvr;
            static IPEndPoint ipEND = new IPEndPoint(0, 1234);

            srvr = new Socket(AddressFamily.InterNetwork , SocketType.Stream, ProtocolType.Tcp);
            srvr.Bind(ipEND);
            srvr.Listen(100);
            Socket s = srvr.Accept();
            while (true)
            {
                byte[] buffer = new byte[1024];
                s.Receive(buffer);
                byte[] msgRecieved = buffer ;
                string message = Encoding.UTF8.GetString(msgRecieved);
                if (message == "QUIT")
                {
                    Console.WriteLine("Client quit !");
                }
                Console.WriteLine(message);

                string greeting = "SalomAleykum Client !";
                buffer = new byte[1024];
                buffer = Encoding.UTF8.GetBytes(greeting);
                s.Send(buffer);
                if (message == "QUIT")
                    break;
            }
            Console.WriteLine("Client quit chat !");
            Console.Read();
            srvr.Close();
            s.Close();
 */