using System.Data;

namespace PayBill.Models
{
    public class Tables
    {
        public int ID { get; set; }
        public string NameTable { get; set; }

        public Tables()
        {

        }
    }

    public class Dishes
    {
        public int ID_Dish { get; set; }
        public string Dish_Name { get; set; }
        public int Is_For_Employee { get; set; }
        public string Dish_Price { get; set; }
        public decimal TotalPrice { get; set; }

        public Dishes()
        {

        }

        public Dishes(DataRow row)
        {
            if (row.Table.Columns.Contains("ID_Dish")) { ID_Dish = int.Parse(row["ID_Dish"].ToString()); }
            if (row.Table.Columns.Contains("Dish_Name")) { Dish_Name = row["Dish_Name"].ToString(); }
            if (row.Table.Columns.Contains("Is_For_Employee")) { Is_For_Employee = int.Parse(row["Is_For_Employee"].ToString()); }
            if (row.Table.Columns.Contains("Dish_Price")) { Dish_Price = row["Dish_Price"].ToString(); }
            if (row.Table.Columns.Contains("TotalPrice")) { TotalPrice = decimal.Parse(row["TotalPrice"].ToString()); }
        }
    }
}