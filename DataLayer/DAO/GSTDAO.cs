using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAO
{
    public class GSTDAO : BaseDAO
    {
        private const decimal gst = 0.15m;

        public GSTDAO() : base(){}

        public decimal getCurrentMultiplier()
        {
            return getMultiplier(DateTime.Now);
        }

        public decimal getMultiplier(DateTime date)
        {
            gst_values val = entities.gst_values.Where(i => i.start_date <= date && (i.end_date >= date || i.end_date == null)).FirstOrDefault();
            if (val == null)
            {
                System.Diagnostics.EventLog.WriteEntry("Serko mail claim", "gst value is null and using default", System.Diagnostics.EventLogEntryType.Warning);
                return gst;
            }
            return val.value;
        }
    }
}
