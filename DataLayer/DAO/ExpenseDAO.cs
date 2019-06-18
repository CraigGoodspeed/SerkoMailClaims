using DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAO
{
    public class ExpenseDAO : BaseDAO
    {
        public ExpenseDAO() : base()
        {
        }

        public Expense CreateExpense(ExpenseDTO dto, Request req)
        {
            Expense toSave = dto.CreateExpense();
            toSave.RequestID = req.id;
            entities.Expenses1.Add(toSave);
            entities.SaveChanges();
            return toSave;
        }

        public Expense getById(int id)
        {
            return entities.Expenses1.Where(i => i.id == id).First();
        }
    }
}
