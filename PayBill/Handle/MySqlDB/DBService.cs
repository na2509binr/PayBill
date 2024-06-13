using PayBill.Data;
using PayBill.Models;
using ZstdSharp.Unsafe;

namespace PayBill.Handle.MySqlDB;

public class DBService
{
    private readonly PayBillDbContext _dbContext;
    public DBService(PayBillDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
