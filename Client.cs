﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server_Socket
{
    internal class Client
    {
        public int Id;
        public Socket socket;

        private int newID = 0;
        public void GetNewID()
        {
            Id = newID;
            newID++;
        }
    }
}
