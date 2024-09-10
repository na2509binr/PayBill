using System.ComponentModel.DataAnnotations;

namespace PayBill.Models;

public class Spending
{
    public string Spending_ID { get; set; }
    public SpendingType SpendingType { get; set; }
    [Required(ErrorMessage = "Số lượng không được để trống")]
    public int Spending_Amount { get; set; }
    [Required(ErrorMessage = "Giá trị không được để trống")]
    public long Spending_Value { get; set; }
    public DateTime CreateTime { get; set; }
}

public enum SpendingType
{
    Wine,
    Grocery,
    Maintain,
    Policy,
    Discount,
    Hdd
}
