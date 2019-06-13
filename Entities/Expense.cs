using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entities
{
    [Serializable()]
    [XmlRoot("expense")]
    public class Expense : BaseExpense
    {
        private const double _GST = 1.15d;
        private const string defaultCostCenter = "UNKNOWN";
        /*
         * <expense>
         *  <cost_centre>DEV002</cost_centre>
         *  <total>1024.01</total>
         * <payment_method>personal card</payment_method>
         * <vendor>Viaduct Steakhouse</vendor>
         * <description>development team’s project end celebration dinner</description>
         * <date>Tuesday 27 April 2017</date>
         *</expense>
         * */

        private string costCenter;
        [XmlElement("cost_center")]
        public string CostCenter 
        {
            get
            {
                return string.IsNullOrEmpty(costCenter) ? defaultCostCenter : costCenter;
            }
            set
            {
                costCenter = value;
            }
        }
        [XmlElement("total")]
        public double Total { get; set; }
        public double TotalExcludingGST
        {
            get
            {
                return Total / _GST;
            }
        }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("payment_method")]
        public string PaymentMethod { get; set; }

        void BaseExpense.Validate()
        {
            if (Total == null)
            {
                base.IsValid = false;
                throw new ExpenseValidationException("The total field needs to be specified.");
            }
        }

        void BaseExpense.Save()
        {
            //TODO: Implement storage.
        }
    }
}
