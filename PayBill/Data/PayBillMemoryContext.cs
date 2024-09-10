using PayBill.Models;

namespace PayBill.Data;

public class PayBillMemoryContext
{
    public Dictionary<int, Dish> Dishes { get; init; } = new Dictionary<int, Dish>();
}
