using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataLayer.DTO
{
    [Serializable()]
    [XmlRoot("expense")]
    public class ExpenseDTO 
    {
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
        [XmlElement("cost_centre")]
        public string CostCentre
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
        public decimal Total { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("payment_method")]
        public string PaymentMethod { get; set; }
        [XmlElement("expense_date")]
        public string ExpenseDate { get; set; }
        [XmlElement("vendor")]
        public string Vendor { get; set; }
        
        
        public Expense CreateExpense()
        {
            Expense toSave = new Expense();
            toSave.CostCentre = this.CostCentre;
            toSave.Description = this.Description;
            toSave.ExpenseDate = parseDate(this.ExpenseDate);
            toSave.PaymentMethod = this.PaymentMethod;
            toSave.Total = this.Total;
            toSave.Vendor = this.Vendor;
            return toSave;
        }
        /// <summary>
        /// this should really be put in a library, but i dont think this is part of the test,
        /// how many date formats i can parse -- use system default for now.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime? parseDate(string date)
        {
            DateTime? toReturn;
            try
            {
                toReturn = System.DateTime.ParseExact(date, "dddd dd MMMM yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                toReturn = null;
            }
            return toReturn;
        }

        public bool isEmpty()
        {
            return
                CostCentre == defaultCostCenter
                &&
                Total == 0
                &&
                string.IsNullOrEmpty(Description)
                &&
                string.IsNullOrEmpty(PaymentMethod)
                &&
                string.IsNullOrEmpty(ExpenseDate)
                &&
                string.IsNullOrEmpty(Vendor);
        }
    }
}
