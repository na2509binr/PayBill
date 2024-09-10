using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PayBill.Models;

public class Tables
{
    public int ID { get; set; }
    [Required(ErrorMessage = "Tên bàn không được để trống")]
    public string NameTable { get; set; }

    public Tables()
    {

    }
}

public class Dish
{
    public int ID_Dish { get; set; }
    [Required(ErrorMessage = "Tên món ăn không được để trống")]
    public string Dish_Name { get; set; }

    [Required(ErrorMessage = "Bạn phải chọn một tùy chọn cho 'Dành cho nhân viên'")]
    public int Is_For_Employee { get; set; }// 1 cho "Có", 0 cho "Không"

    [Required(ErrorMessage = "Giá món ăn là bắt buộc")]
    [Range(1, long.MaxValue, ErrorMessage = "Giá món ăn phải là số dương")]
    public long Dish_Price { get; set; }
    public long TotalPrice { get; set; }

    public Dish()
    {

    }

    public Dish(DataRow row)
    {
        if (row.Table.Columns.Contains("ID_Dish")) { ID_Dish = int.Parse(row["ID_Dish"].ToString()); }
        if (row.Table.Columns.Contains("Dish_Name")) { Dish_Name = row["Dish_Name"].ToString(); }
        if (row.Table.Columns.Contains("Is_For_Employee")) { Is_For_Employee = int.Parse(row["Is_For_Employee"].ToString()); }
        if (row.Table.Columns.Contains("Dish_Price")) { Dish_Price = long.Parse(row["Dish_Price"].ToString()); }
        if (row.Table.Columns.Contains("TotalPrice")) { TotalPrice = long.Parse(row["TotalPrice"].ToString()); }
    }
}