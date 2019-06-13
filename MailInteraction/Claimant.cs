using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MailInteraction
{
    public abstract class Claimant
    {
        public string claimant { get; set; }
    }
}
