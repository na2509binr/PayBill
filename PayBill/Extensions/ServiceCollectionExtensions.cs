using PayBill.Data;
using PayBill.Services;

namespace PayBill.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSingletonService(this IServiceCollection services)
    {
        services.AddSingleton<IReceiptService, ReceiptService>();
        services.AddSingleton<PayBillMemoryContext>();
        return services;
    }
}
