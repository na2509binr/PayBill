using System.Data;

namespace PayBill.Models;

public class Employee
{
    public string ID_Employee { get; set; }
    public string Employee_Name { get; set; }

    public Employee()
    {
        
    }

    public Employee(DataRow row)
    {
        if (row.Table.Columns.Contains("ID_Employee")) { ID_Employee = row["ID_Employee"].ToString(); }
        if (row.Table.Columns.Contains("Employee_Name")) { Employee_Name = row["Employee_Name"].ToString(); }
    }
}

public class Receipt_Employee
{
    public string ID_Receipt_Details { get; set; }
    public string ID_Receipt { get; set; }
    public int ID_Dish { get; set; }
    public string ID_Employee { get; set; }
    public int Quantity { get; set; }
    public string Create_Date { get; set; }

    public Receipt_Employee()
    {

    }

    public Receipt_Employee(DataRow row)
    {
        if (row.Table.Columns.Contains("ID_Receipt_Details")) { ID_Receipt_Details = row["ID_Receipt_Details"].ToString(); }
        if (row.Table.Columns.Contains("ID_Receipt")) { ID_Receipt = row["ID_Receipt"].ToString(); }
        if (row.Table.Columns.Contains("ID_Dish")) { ID_Dish = int.Parse(row["ID_Dish"].ToString()); }
        if (row.Table.Columns.Contains("ID_Employee")) { ID_Employee = row["ID_Employee"].ToString(); }
        if (row.Table.Columns.Contains("Quantity")) { Quantity = int.Parse(row["Quantity"].ToString()); }
        if (row.Table.Columns.Contains("Create_Date")) { Create_Date = row["Create_Date"].ToString(); }
    }
}
