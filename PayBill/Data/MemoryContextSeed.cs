using PayBill.Handle.MySqlDB;
using System.Data.Entity;

namespace PayBill.Data;

public class MemoryContextSeed
{
    public void SeedAsync(PayBillDbContext dbContext, PayBillMemoryContext memoryContext, IServiceProvider services)
    {
        SeedDish(dbContext, memoryContext);
    }

    private void SeedDish(PayBillDbContext dbContext, PayBillMemoryContext memoryContext)
    {
        var dishes = dbContext.Dishes.ToList();

        foreach (var dish in dishes)
        {
            var key = dish.ID_Dish;
            if (!memoryContext.Dishes.ContainsKey(key))
                memoryContext.Dishes.Add(key, dish);
        }
    }

}
