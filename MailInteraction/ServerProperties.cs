using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailInteraction
{
    public class ServerProperties
    {
        public string url{get; set;}
        public int port { get; set; }
        public bool useSSL { get; set; }
        
        public ServerProperties(string url, int port, bool useSSL)
        {
            this.url = url;
            this.port = port;
            this.useSSL = useSSL;
        }
    }
}
