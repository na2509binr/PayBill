using PayBill.Models;
using System.Data;

namespace PayBill.Handle.OracleDB
{
    public class clsDb
    {
        private readonly clsDbDAO _clsDb = new clsDbDAO();
        //private readonly List<Dishes>;

        public Dishes[] GetList_Dish(int p_typeCust)
        {
            try
            {
                var dtReturn = _clsDb.GetData_Dish(p_typeCust);
                if (dtReturn == null) return null;

                var rows = dtReturn.Rows;
                var itemsReturn = new Dishes[rows.Count];

                for (int i = 0; i < rows.Count; i++)
                {
                    itemsReturn[i] = new Dishes(rows[i]);
                }

                return itemsReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Employee[] GetList_Employee()
        {
            try
            {
                var dtReturn = _clsDb.GetData_Employee();
                if (dtReturn == null) return null;

                var rows = dtReturn.Rows;
                var itemsReturn = new Employee[rows.Count];

                for (int i = 0; i < rows.Count; i++)
                {
                    itemsReturn[i] = new Employee(rows[i]);
                }

                return itemsReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool ReceiptDetail_i(List<Receipt_Details> list)
        {
            bool checkReturn = false;
            var dtReturn = _clsDb.ReceiptDetail_i(list);
            if (dtReturn == list.Count())
            {
                checkReturn = true;
            }
            else
            {
                checkReturn = false;
            }
            return checkReturn;
        }

        public bool ReceiptEmployee_i(List<Receipt_Employee> list)
        {
            bool checkReturn = false;
            var dtReturn = _clsDb.ReceiptEmployee_i(list);
            if (dtReturn == list.Count())
            {
                checkReturn = true;
            }
            else
            {
                checkReturn = false;
            }
            return checkReturn;
        }

        public bool Receipt_i(Receipt model)
        {
            bool checkReturn = false;
            var dtReturn = _clsDb.Receipt_i(model);
            if (dtReturn == 1)
            {
                checkReturn = true;
            }
            else
            {
                checkReturn = false;
            }
            return checkReturn;
        }
    }
}
