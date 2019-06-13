using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailInteraction.Claim
{
    class ClaimAttachment : Claimant
    {
        
        public List<byte[]> claims { get; }
    }
}
