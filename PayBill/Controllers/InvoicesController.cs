using Microsoft.AspNetCore.Mvc;
using PayBill.Data;
using PayBill.Models;

namespace PayBill.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly PayBillDbContext _dbContext;
        private readonly PayBillMemoryContext _memoryContext;

        public InvoicesController(PayBillDbContext context, PayBillMemoryContext memoryContext)
        {
            _dbContext     = context;
            _memoryContext = memoryContext;
        }

        public IActionResult PrintPreview(string receiptId)
        {
            // Giả sử bạn có Receipt và ReceiptDetails trong database
            var receipt = _dbContext.Receipts.FirstOrDefault(r => r.ID_Receipt == receiptId);
            var receiptDetails = _dbContext.Receipt_Details.Where(d => d.ID_Receipt == receiptId).ToList();
            var InvoiceItems = new List<InvoiceViewDetail>();
            foreach (var item in receiptDetails) {
                var i = 1;
                var hasDish = _memoryContext.Dishes.TryGetValue(item.ID_Dish, out var dish);
                if (hasDish) 
                {
                    var invoiceDetail = new InvoiceViewDetail
                    {
                        Index     = i,
                        DishName  = dish.Dish_Name,
                        Quantity  = item.Quantity,
                        DishPrice = dish.Dish_Price,

                    };
                    InvoiceItems.Add(invoiceDetail);
                }
                i++;
            }
            var viewModel = new InvoiceViewModel
            {
                InvoiceNumber = receipt.ID_Receipt.ToString(),
                CreatedDate = receipt.Create_Date,
                PaymentMethod = receipt.Payment_Methods == 0 ? "Tiền mặt" : "Thẻ",
                InvoiceItems = InvoiceItems,
                TotalAmount = receipt.Total_Price
            };

            return View("Invoice", viewModel);
        }
    }
}
