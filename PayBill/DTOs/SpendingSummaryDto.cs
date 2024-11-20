namespace PayBill.DTOs;

public class SpendingSummaryDto
{
    public string SpendingDay { get; set; }
    public long TotalSpending { get; set; }
    public long TotalWine { get; set; }
    public long TotalGlocery { get; set; }
    public long TotalSacrificialOfferings { get; set; }
    public long TotalBillExtract { get; set; }
    public long TotalLoanEmployee { get; set; }
    public long TotalCsvc { get; set; }
    public long TotalHdd { get; set; }
}
