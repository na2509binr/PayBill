using PayBill.Models;

namespace PayBill.Services;

public interface IReceiptService
{
    void PrintReceipt(Receipt modelReceipt, List<Receipt_Details> receiptDetails);
}
