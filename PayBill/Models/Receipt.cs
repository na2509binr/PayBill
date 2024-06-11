using System.Data;

namespace PayBill.Models
{
    public class Receipt
    {
        public string ID_Receipt { get; set; }
        public string Create_Date { get; set; }
        public decimal Total_Price { get; set; }

        public Receipt()
        {
            
        }

        public Receipt(DataRow row)
        {
            if (row.Table.Columns.Contains("ID_Receipt")) { ID_Receipt = row["ID_Receipt"].ToString(); }
            if (row.Table.Columns.Contains("Create_Date")) { Create_Date = row["Create_Date"].ToString(); }
            if (row.Table.Columns.Contains("Total_Price")) { Total_Price = decimal.Parse(row["Total_Price"].ToString()); }
        }
    }

    public class Receipt_Details
    {
        public string ID_Receipt_Details { get; set; }
        public string ID_Receipt { get; set; }
        public int ID_Dish { get; set; }
        public int Quantity { get; set; }
        //public decimal Total_Price { get; set; }
        public string Create_Date { get; set; }

        public Receipt_Details()
        {

        }

        public Receipt_Details(DataRow row)
        {
            if (row.Table.Columns.Contains("ID_Receipt_Details")) { ID_Receipt_Details = row["ID_Receipt_Details"].ToString(); }
            if (row.Table.Columns.Contains("ID_Receipt")) { ID_Receipt = row["ID_Receipt"].ToString(); }
            if (row.Table.Columns.Contains("ID_Dish")) { ID_Dish = int.Parse(row["ID_Dish"].ToString()); }
            if (row.Table.Columns.Contains("Quantity")) { Quantity = int.Parse(row["Quantity"].ToString()); }
            if (row.Table.Columns.Contains("Create_Date")) { Create_Date = row["Create_Date"].ToString(); }
        }
    }
}
