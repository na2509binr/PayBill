using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using PayBill.Data;
using PayBill.Handle.OracleDB;
using PayBill.Models;
using System.Diagnostics;
using X.PagedList;

namespace PayBill.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PayBillDbContext _dbContext;
        clsDb _clsDb;

        public HomeController(ILogger<HomeController> logger, PayBillDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Booking()
        {
            //_clsDb = new clsDb();
            //Dish[] arrDishes = _clsDb.GetList_Dish(-1);
            var dishes = _dbContext.Dishes.ToArray();
            var tables = _dbContext.Tables.ToArray();
            /*Tables[] arr = new Tables[]
            {
                new Tables {ID= 9, NameTable="Bàn 9"},
                new Tables {ID= 2, NameTable="Bàn 2"},
                new Tables {ID= 5, NameTable="Bàn 5"},
                new Tables {ID= 7, NameTable="Bàn 7"},
                new Tables {ID= 1, NameTable="Bàn 1"},
                new Tables {ID= 3, NameTable="Bàn 3"},
                new Tables {ID= 8, NameTable="Bàn 8"},
                new Tables {ID= 4, NameTable="Bàn 4"},
                new Tables {ID= 6, NameTable="Bàn 6"}
            };

            Dish[] arrDish = new Dish[]
            {
                new Dish {ID_Dish = 9, Dish_Name="Rượu Sake" ,Dish_Price = "8000"},
                new Dish {ID_Dish = 2, Dish_Name="Banh my", Dish_Price = "230000"},
                new Dish {ID_Dish = 5, Dish_Name="Sushi", Dish_Price = "6000"},
                new Dish {ID_Dish = 7, Dish_Name="Sashimi", Dish_Price = "100000"},
                new Dish {ID_Dish = 1, Dish_Name="Bánh nhân bạch tuộc Takoyaki", Dish_Price = "20000"},
                new Dish {ID_Dish = 3, Dish_Name="Lẩu bò Shabu", Dish_Price = "9000"},
                new Dish {ID_Dish = 8, Dish_Name="Cơm cà ri", Dish_Price = "65000"},
                new Dish {ID_Dish = 4, Dish_Name="Bach tuoc Nhat", Dish_Price = "2000"},
                new Dish {ID_Dish = 6, Dish_Name="Okonomiyaki monjayaki", Dish_Price = "8000"}
            };*/

            GetDataToViewModel getDataModel = new GetDataToViewModel();
            getDataModel.tablesList = tables.ToPagedList<Tables>();
            getDataModel.dishesList = dishes.ToPagedList<Dish>();
            getDataModel.message = TempData["Message"] as string;

            return View("Booking", getDataModel);
        }

        public IActionResult Employee()
        {
            _clsDb = new clsDb();
            Dish[] arrDishes = _dbContext.Dishes.Where(c=> c.Is_For_Employee == 1).ToArray();
            //Employee[] arrEmployee = _clsDb.GetList_Employee();
            Employee[] arrEmployee = _dbContext.Employees.ToArray();

            /*Employee[] arr = new Employee[]
            {
                new Employee {ID_Employee = "Emp001", Employee_Name="Đàm Bá Bằng"},
                new Employee {ID_Employee = "Emp011", Employee_Name="Phạm Việt Huy"},
                new Employee {ID_Employee = "Emp061", Employee_Name="Phạm Như Hạ Uyên"}
            };

            Dish[] arrDish = new Dish[]
            {
                new Dish {ID_Dish = 9, Dish_Name="Rượu Sake" ,Dish_Price = "8000"},
                new Dish {ID_Dish = 2, Dish_Name="Banh my", Dish_Price = "230000"},
                new Dish {ID_Dish = 5, Dish_Name="Sushi", Dish_Price = "6000"},
                new Dish {ID_Dish = 7, Dish_Name="Sashimi", Dish_Price = "100000"},
                new Dish {ID_Dish = 1, Dish_Name="Bánh nhân bạch tuộc Takoyaki", Dish_Price = "20000"},
                new Dish {ID_Dish = 3, Dish_Name="Lẩu bò Shabu", Dish_Price = "9000"},
                new Dish {ID_Dish = 8, Dish_Name="Cơm cà ri", Dish_Price = "65000"},
                new Dish {ID_Dish = 4, Dish_Name="Bach tuoc Nhat", Dish_Price = "2000"},
                new Dish {ID_Dish = 6, Dish_Name="Okonomiyaki monjayaki", Dish_Price = "8000"}
            };*/

            GetDataToViewModel getDataModel = new GetDataToViewModel();
            getDataModel.employeeList = arrEmployee.ToPagedList<Employee>();
            getDataModel.dishesList = arrDishes.ToPagedList<Dish>();
            getDataModel.message = TempData["Message"] as string;

            return View("Employee", getDataModel);
        }

        #region Read HTML to get List Receipt
        [HttpPost]
        public IActionResult GetReceiptDetails(string html)
        {
            try
            {
                var dteNow = DateTime.Now;
                string message = "";
                string idTable = "";
                long totalMoney = 0;
                var tableDataList = new List<Receipt_Details>();


                // Load HTML vào một đối tượng HtmlDocument
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var bill = doc.DocumentNode.SelectNodes("//span[contains(@class, 'bill')]");          
                if (bill != null)
                {
                    Console.WriteLine("Các giá trị của thẻ có class 'bill':");
                    foreach (var node in bill)
                    {
                        totalMoney = long.Parse(node.InnerText);
                    }
                }


                var buttonActive = doc.DocumentNode.SelectNodes("//button[contains(@class, 'acti')]");
                if (buttonActive != null)
                {
                    foreach (var button in buttonActive)
                    {
                        // Đi ngược lên để tìm thẻ <tr> chứa thẻ <button>
                        var row = button.Ancestors("tr").FirstOrDefault();
                        if (row != null)
                        {
                            var firstColumn = row.SelectSingleNode("td[1]");
                            if (firstColumn != null)
                            {
                                idTable = firstColumn.InnerText;
                            }
                        }
                    }
                }

                var idReceipt = dteNow.ToString("dd/MM/yyyy HH:mm:ss:ffff") + idTable;
                Receipt modelReceipt = new Receipt()
                {
                    ID_Receipt = idReceipt,
                    Create_Date = dteNow.ToString("dd/MM/yyyy"),
                    Total_Price = totalMoney
                };

                var tablesPopup = doc.DocumentNode.SelectNodes("//table[contains(@class, 'dataGridPopup')]");
                // Kiểm tra xem có bảng nào khớp không
                if (tablesPopup != null)
                {
                    foreach (var table in tablesPopup)
                    {
                        // Lặp qua từng dòng trong bảng
                        foreach (var body in table.SelectNodes("tbody"))
                        {
                            foreach (var row in body.SelectNodes("tr"))
                            {
                                var rowData = new Receipt_Details();

                                // Lặp qua từng ô trong dòng
                                var cells = row.SelectNodes("td");
                                if (cells != null) // Kiểm tra có đủ số ô không
                                {
                                    var result  = cells[3].InnerText.Trim();
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
                }

                if (tableDataList.Count > 0)
                {
                    /*_clsDb = new clsDb();
                    var blReceipt = _clsDb.Receipt_i(modelReceipt);
                    
                    var blDetail = _clsDb.ReceiptDetail_i(tableDataList);
                    if (blReceipt && blDetail)
                    {
                        message = "Thanh toán thành công!";
                        TempData["Message"] = message;
                    }*/
                    try
                    {
                        _dbContext.Receipts.Add(modelReceipt);
                        _dbContext.Receipt_Details.AddRange(tableDataList);
                        _dbContext.SaveChanges();
                        message = "Thanh toán thành công!";
                        TempData["Message"] = message;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                return RedirectToAction("Booking");
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public IActionResult GetReceiptEmployee(string html)
        {
            try
            {
                var dteNow = DateTime.Now;
                string message = "";
                string idEmp = "";
                long totalMoney = 0;
                var tableDataList = new List<Receipt_Employee>();


                // Load HTML vào một đối tượng HtmlDocument
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var bill = doc.DocumentNode.SelectNodes("//span[contains(@class, 'bill')]");
                if (bill != null)
                {
                    Console.WriteLine("Các giá trị của thẻ có class 'bill':");
                    foreach (var node in bill)
                    {
                        totalMoney = long.Parse(node.InnerText);
                    }
                }


                var buttonActive = doc.DocumentNode.SelectNodes("//button[contains(@class, 'acti')]");
                if (buttonActive != null)
                {
                    foreach (var button in buttonActive)
                    {
                        // Đi ngược lên để tìm thẻ <tr> chứa thẻ <button>
                        var row = button.Ancestors("tr").FirstOrDefault();
                        if (row != null)
                        {
                            var firstColumn = row.SelectSingleNode("td[1]");
                            if (firstColumn != null)
                            {
                                idEmp = firstColumn.InnerText;
                            }
                        }
                    }
                }
                var idReceipt = dteNow.ToString("dd/MM/yyyy HH:mm:ss:ffff") + idEmp;
                Receipt modelReceipt = new Receipt()
                {
                    ID_Receipt = idReceipt,
                    Create_Date = dteNow.ToString("dd/MM/yyyy"),
                    Total_Price = totalMoney
                };

                var tablesPopup = doc.DocumentNode.SelectNodes("//table[contains(@class, 'dataGridPopup')]");
                // Kiểm tra xem có bảng nào khớp không
                if (tablesPopup != null)
                {
                    foreach (var table in tablesPopup)
                    {
                        // Lặp qua từng dòng trong bảng
                        foreach (var body in table.SelectNodes("tbody"))
                        {
                            foreach (var row in body.SelectNodes("tr"))
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
                                        rowData.ID_Employee = idEmp;
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
                }

                if (tableDataList.Count > 0)
                {
                    /*_clsDb = new clsDb();
                    var blReceipt = _clsDb.Receipt_i(modelReceipt);
                    var blEmployee = _clsDb.ReceiptEmployee_i(tableDataList);
                    if (blReceipt && blEmployee)
                    {
                        message = "Thanh toán thành công!";
                        TempData["Message"] = message;
                    }*/
                    try
                    {
                        _dbContext.Receipt_Employees.AddRange(tableDataList);
                        _dbContext.SaveChanges();
                        message = "Thanh toán thành công!";
                        TempData["Message"] = message;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                return RedirectToAction("Employee");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "sa123")
            {
                return RedirectToAction("Index", "Home");
            }
            else if (username == null && password == null)
            {

            }
            else if (username == null || password == null)
            {
                ViewBag.Error = "Vui lòng nhập tên tài khoản hoặc mật khẩu!";
            }
            else if (username != "admin" || password != "password")
            {
                ViewBag.Error = "Tên tài khoản hoặc mật khẩu không đúng!";
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
