using PayBill.DTOs;

namespace PayBill.Services;

public interface IDbServices
{
    List<SpendingSummaryDto> GetSpendingSummary();
    List<ReceiptSummaryDto> GetReceiptSummary();
}
