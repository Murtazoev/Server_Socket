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
        // Task
        // Async
        // Sync
        // Thread
        Socket server;
        IPEndPoint ipEND = new IPEndPoint(0, 1234);
        List<Client> clients;
        public async void Start()
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipEND);
            server.Listen(1000);
            Console.WriteLine("Server Started !");
            Console.WriteLine($"Main thread: {Thread.CurrentThread.ManagedThreadId}");
            clients = new List<Client>();
        }

        public async Task AcceptCalls()
        {
            Socket newSocket = await server.AcceptAsync();
            Client newClient = new Client(newSocket);
            clients.Add(newClient);
            Console.WriteLine("Accepted client: " + newClient.Id);
            HandleClient(newClient);
            // Task 
        }

        static async Task HandleClient(Client client)
        {
            Console.WriteLine("Client connected: " + client.socket.RemoteEndPoint);

            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = await client.socket.ReceiveAsync(buffer, SocketFlags.None);
                    if (bytesRead > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine("Received from client " + client.Id + ": " + message);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Client disconnected: " + client.Id);
                // clients.Remove(client);
                client.socket.Close();
            }
        }

        public async Task Update() {
            await AcceptCalls();
            // await ReceiveMessages();
        }

        // private async Task ReceiveMessages()
        // {
        //     if (clients.Count == 0)
        //         return;
        //     foreach (Client client in clients)
        //     {
        //         byte[] buffer = new byte[1024];
        //         await client.socket.ReceiveAsync(buffer);
        //         Console.WriteLine("Client id" + client.Id);
        //         string messageReceived = Encoding.UTF8.GetString(buffer);
        //         if (messageReceived != null)
        //             Console.WriteLine("Client " + client.Id + ": " + messageReceived + " " + clients.Count);
        //     }
        // }
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