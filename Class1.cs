using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server_Socket
{
    internal class Server
    {
        static Socket server;
        static Socket client;
        static IPEndPoint ipEND = new IPEndPoint(0 , 1234);
        static Client newClient;
        List<Client> clients;
        public void Start()
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipEND);
            server.Listen(1000);
            Console.WriteLine("Server Started !");
        }

        public async void AcceptCalls()
        {
            Client newClient;
            newClient = new Client();
            await server.AcceptAsync(newClient.socket);
            if (newClient.socket == null)
                return;
            Console.WriteLine("Connected !");
            newClient.GetNewID();
            clients.Add(newClient);
            Console.WriteLine("Client " + newClient.Id + " : Connected !");
        }
        public void Greeting()
        {

        }
        public void Update()
        {
            AcceptCalls();
            if (clients != null)
                ReceiveMessages();
        }

        private async void ReceiveMessages()
        {
            foreach (Client i in clients)
            {
                byte[] buffer = new byte[1024];
                await i.socket.ReceiveAsync(buffer);
                string messageReceived = Encoding.UTF8.GetString(buffer);
                if (messageReceived != null)
                    Console.WriteLine("Client " + i.Id + ": " + messageReceived);
            }
        }
    }
}

/*
 
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Timers;
using System.Runtime.Intrinsics.X86;

namespace Server_Socket
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
            srvr = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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

        private static void TimerEventProcessor(object myObject, EventArgs myEventsArgs)
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
                Console.WriteLine("Client 1 : " + message);
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
*/