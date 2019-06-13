using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailInteraction
{
    public class ServerProperties
    {
        public string url{get; private set;}
        public int port { get; private set; }
        public bool useSSL { get; private set; }
        
        public ServerProperties(string url, int port, bool useSSL)
        {
            this.url = url;
            this.port = port;
            this.useSSL = useSSL;
        }
    }
}
