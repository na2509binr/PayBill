using PayBill.Data;

namespace PayBill.Extensions;

public static class HostExtensions
{
    public static IHost InitialMemory(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            using (var dbContext = services.GetService<PayBillDbContext>())
            {
                var memoryContext = services.GetService<PayBillMemoryContext>();
                try
                {
                    new MemoryContextSeed().SeedAsync(dbContext, memoryContext, services);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        return host;
    }
}
