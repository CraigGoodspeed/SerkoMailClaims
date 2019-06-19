using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer;
using BusinessLayer.Message;

namespace Tests
{
    [TestClass]
    public class CheckMessageCreation
    {
        [TestMethod]
        public void CheckExpenseMessage()
        {
            Expense exp = new Expense();
            exp.PaymentMethod = "asd";
            exp.Total = 33m;
            exp.CostCentre = "zaza";
            ExpenseMessage msg = new ExpenseMessage();
            msg.getResponse(exp);
        }
    }
}
