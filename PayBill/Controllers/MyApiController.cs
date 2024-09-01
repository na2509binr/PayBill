using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using PayBill.Data;
using PayBill.Handle.OracleDB;
using PayBill.Models;
using X.PagedList;

namespace PayBill.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class MyApiController : ControllerBase
    {
        private readonly ILogger<MyApiController> _logger;
        private readonly PayBillDbContext _dbContext;
        ReceiptDetailsResponse response = new ReceiptDetailsResponse();
        clsDb _clsDb;

        public MyApiController(ILogger<MyApiController> logger, PayBillDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost("GetReceiptDetails")]
        public IActionResult GetReceiptDetails([FromBody] ReceiptRequest request)
        {
            try
            {
                var dteNow = DateTime.Now;
                long totalMoney = 0;
                var tableDataList = new List<Receipt_Details>();

                // Load HTML vào một đối tượng HtmlDocument
                var doc = new HtmlDocument();
                doc.LoadHtml(request.html);

                totalMoney = long.Parse(request.billInput.Replace(",", ""));

                var idReceipt = dteNow.ToString("dd/MM/yyyy HH:mm:ss:ffff") + request.idTable;


                Receipt modelReceipt = new Receipt()
                {
                    ID_Receipt = idReceipt,
                    Create_Date = dteNow.ToString("dd/MM/yyyy"),
                    Total_Price = totalMoney,
                    Payment_Methods = int.Parse(request.paymentMethod)
                };

                var tablesPopup = doc.DocumentNode.SelectNodes("tbody");
                if (tablesPopup != null)
                {
                    foreach (var table in tablesPopup)
                    {

                        foreach (var row in table.SelectNodes("tr"))
                        {
                            var rowData = new Receipt_Details();

                            // Lặp qua từng ô trong dòng
                            var cells = row.SelectNodes("td");
                            if (cells != null) // Kiểm tra có đủ số ô không
                            {
                                var result = cells[3].InnerText.Trim();
                                var quantity = new string(result.Where(c => !char.IsWhiteSpace(c) && c != '-' && c != '+').ToArray());
                                if (int.Parse(quantity) > 0)
                                {
                                    var idDish = cells[0].InnerText.Trim();
                                    var idReceiptDetails = idReceipt + quantity + idDish;

                                    rowData.ID_Receipt_Details = idReceiptDetails;
                                    rowData.ID_Receipt = idReceipt;
                                    rowData.ID_Dish = int.Parse(idDish);
                                    rowData.Quantity = int.Parse(quantity);
                                    rowData.Create_Date = dteNow.ToString("dd/MM/yyyy");
                                }
                            }

                            if (rowData.ID_Receipt != null && rowData.ID_Receipt != null)
                            {
                                tableDataList.Add(rowData);
                            }
                        }
                    }
                }

                if (tableDataList.Count > 0)
                {
                    try
                    {
                        _dbContext.Receipts.Add(modelReceipt);
                        _dbContext.Receipt_Details.AddRange(tableDataList);
                        _dbContext.SaveChanges();
                        response.message = "Thanh toán thành công!";
                    }
                    catch (Exception ex)
                    {
                        response.message = "Thanh toán thất bại!";
                    }
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = "Thanh toán thất bại!";
                return Ok(response);
            }
        }
        
        [HttpPost("GetReceiptEmployee")]
        public IActionResult GetReceiptEmployee([FromBody] ReceiptRequest request)
        {
            try
            {
                var dteNow = DateTime.Now;
                long totalMoney = 0;
                var tableDataList = new List<Receipt_Employee>();

                // Load HTML vào một đối tượng HtmlDocument
                var doc = new HtmlDocument();
                doc.LoadHtml(request.html);

                totalMoney = long.Parse(request.billInput.Replace(",", ""));

                var idReceipt = dteNow.ToString("dd/MM/yyyy HH:mm:ss:ffff") + request.idTable;


                Receipt modelReceipt = new Receipt()
                {
                    ID_Receipt = idReceipt,
                    Create_Date = dteNow.ToString("dd/MM/yyyy"),
                    Total_Price = totalMoney,
                    Payment_Methods = int.Parse(request.paymentMethod)
                };

                var tablesPopup = doc.DocumentNode.SelectNodes("tbody");
                if (tablesPopup != null)
                {
                    foreach (var table in tablesPopup)
                    {

                        foreach (var row in table.SelectNodes("tr"))
                        {
                            var rowData = new Receipt_Employee();

                            // Lặp qua từng ô trong dòng
                            var cells = row.SelectNodes("td");
                            if (cells != null) // Kiểm tra có đủ số ô không
                            {
                                var result = cells[3].InnerText.Trim();
                                var quantity = new string(result.Where(c => !char.IsWhiteSpace(c) && c != '-' && c != '+').ToArray());
                                if (int.Parse(quantity) > 0)
                                {
                                    var idDish = cells[0].InnerText.Trim();
                                    var idReceiptDetails = idReceipt + quantity + idDish;

                                    rowData.ID_Receipt_Details = idReceiptDetails;
                                    rowData.ID_Receipt = idReceipt;
                                    rowData.ID_Dish = int.Parse(idDish);
                                    rowData.ID_Employee = request.idTable;
                                    rowData.Quantity = int.Parse(quantity);
                                    rowData.Create_Date = dteNow.ToString("dd/MM/yyyy");
                                }
                            }

                            if (rowData.ID_Receipt != null && rowData.ID_Receipt != null)
                            {
                                tableDataList.Add(rowData);
                            }
                        }
                    }
                }

                if (tableDataList.Count > 0)
                {
                    try
                    {
                        _dbContext.Receipts.Add(modelReceipt);
                        _dbContext.Receipt_Employees.AddRange(tableDataList);
                        _dbContext.SaveChanges();
                        response.message = "Thanh toán thành công!";
                    }
                    catch (Exception ex)
                    {
                        response.message = "Thanh toán thất bại!";
                    }
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.message = "Thanh toán thất bại!";
                return Ok(response);
            }
        }
    }
}
