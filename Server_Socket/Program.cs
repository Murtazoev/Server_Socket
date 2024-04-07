using System;
using System.Net;
using System.Net.Sockets;

namespace Server_Socket
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Server server = new Server();
            server.Start();
            while (true)
                await server.Update();
        }
    }
}