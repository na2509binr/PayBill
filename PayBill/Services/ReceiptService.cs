using PayBill.Models;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using PayBill.Data;

namespace PayBill.Services;

public class ReceiptService : IReceiptService
{
    private readonly PayBillMemoryContext _memoryContext;
    public ReceiptService(PayBillMemoryContext memoryContext)
    {
        _memoryContext = memoryContext;
    }

    public void PrintReceipt(Receipt modelReceipt, List<Receipt_Details> receiptDetails)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                // Thiết lập font và vị trí in
                Font font = new Font("Arial", 12);
                float yPos = 100;

                // In thông tin hóa đơn chính
                e.Graphics.DrawString($"Hóa đơn: {modelReceipt.ID_Receipt}", font, Brushes.Black, new PointF(100, yPos));
                yPos += 30;
                e.Graphics.DrawString($"Ngày tạo: {modelReceipt.Create_Date}", font, Brushes.Black, new PointF(100, yPos));
                yPos += 30;
                e.Graphics.DrawString($"Tổng tiền: {modelReceipt.Total_Price} VND", font, Brushes.Black, new PointF(100, yPos));
                yPos += 30;
                e.Graphics.DrawString($"Phương thức thanh toán: {(modelReceipt.Payment_Methods == 0 ? "Tiền mặt" : "Thẻ")}", font, Brushes.Black, new PointF(100, yPos));
                yPos += 50;

                // In chi tiết hóa đơn
                foreach (var detail in receiptDetails)
                {
                    var hasDish = _memoryContext.Dishes.TryGetValue(detail.ID_Dish, out var dish);
                    if (hasDish)
                    {
                        e.Graphics.DrawString($"Tên hàng: {dish.Dish_Name}", font, Brushes.Black, new PointF(100, yPos));
                        yPos += 30;
                        e.Graphics.DrawString($"Số lượng: {detail.Quantity}", font, Brushes.Black, new PointF(100, yPos));
                        yPos += 30;
                        e.Graphics.DrawString($"Giá tiền: {dish.Dish_Price} VND", font, Brushes.Black, new PointF(100, yPos));
                        yPos += 50;
                    }
                }
            };

            // Bắt đầu in
            printDocument.Print();
        }
    }
}
