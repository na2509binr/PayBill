namespace PayBill.Models;

public class InvoiceViewModel
{
    public string InvoiceNumber { get; set; }
    public string CreatedDate { get; set; }
    public string PaymentMethod { get; set; }
    public List<InvoiceViewDetail> InvoiceItems { get; set; }
    public decimal TotalAmount { get; set; }
}