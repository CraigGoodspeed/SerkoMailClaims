//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public Request()
        {
            this.Expenses = new HashSet<Expense>();
        }
    
        public int id { get; set; }
        public string MailFrom { get; set; }
        public byte[] MailContent { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}