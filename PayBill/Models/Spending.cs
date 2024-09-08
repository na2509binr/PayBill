namespace PayBill.Models;

public class Spending
{
    public string Spending_ID { get; set; }
    public SpendingType SpendingType { get; set; }
    public int Spending_Amount { get; set; }
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
