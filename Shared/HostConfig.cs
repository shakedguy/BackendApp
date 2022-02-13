using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BackendApp.Shared
{
    public class HostConfig
    {
        public static IPAddress Domain { get; set; } = Dns.GetHostEntry("localhost").AddressList[0];
        public static int Port { get; set; }
        public static string CrtPath { get; set; }
        public static string CrtPassword { get; set; }
    }
}
