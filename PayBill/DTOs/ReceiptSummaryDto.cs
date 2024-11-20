namespace PayBill.DTOs;

public class ReceiptSummaryDto
{
    public string Day { get; set; }
    public long TotalPrice { get; set; }
    public long TotalCashAmount { get; set; }
    public long TotalBankTransfer { get; set; }
    public long TotalCreditCard { get; set; }
}
