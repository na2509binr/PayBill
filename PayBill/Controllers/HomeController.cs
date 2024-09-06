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
            var dishes = _dbContext.Dishes.ToArray();
            var tables = _dbContext.Tables.ToArray();

            GetDataToViewModel getDataModel = new GetDataToViewModel();
            getDataModel.tablesList = tables.ToPagedList<Tables>();
            getDataModel.dishesList = dishes.ToPagedList<Dish>();
            getDataModel.message = TempData["Message"] as string;

            return View("Booking", getDataModel);
        }

        public IActionResult Employee()
        {
            _clsDb = new clsDb();
            Dish[] arrDishes = _dbContext.Dishes.Where(c => c.Is_For_Employee == 1).ToArray();
            Employee[] arrEmployee = _dbContext.Employees.ToArray();

            GetDataToViewModel getDataModel = new GetDataToViewModel();
            getDataModel.employeeList = arrEmployee.ToPagedList<Employee>();
            getDataModel.dishesList = arrDishes.ToPagedList<Dish>();
            getDataModel.message = TempData["Message"] as string;

            return View("Employee", getDataModel);
        }

        public IActionResult History(string fromdte, string todte)
        {
            try
            {
                Receipt[] receipts;
                receipts = _dbContext.Receipts.ToArray();
                var receiptsDetail = _dbContext.Receipt_Details.ToArray();

                var query = from t1 in _dbContext.Receipt_Employees
                            join t2 in _dbContext.Dishes on t1.ID_Dish equals t2.ID_Dish
                            join t3 in _dbContext.Employees on t1.ID_Employee equals t3.ID_Employee
                            select new ReceiptEmployeeViewModel
                            {
                                ID = t1.ID_Receipt,
                                Dish_Name = t2.Dish_Name,
                                Emp_Name = t3.Employee_Name,
                                Create_Date = t1.Create_Date,
                                Quantity = t1.Quantity
                            };

                ReceiptEmployeeViewModel[] receiptEmployeeModel = query.ToArray();


                GetDataToViewModel getDataModel = new GetDataToViewModel();
                getDataModel.receiptList = receipts.ToPagedList<Receipt>();
                getDataModel.receiptDetailList = receiptsDetail.ToPagedList<Receipt_Details>();
                getDataModel.receiptEmployeeModelList = receiptEmployeeModel.ToPagedList<ReceiptEmployeeViewModel>();
                getDataModel.message = TempData["Message"] as string;
                return View("History", getDataModel);
            }
            catch (Exception)
            {
                return View("History");
            }
        }
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
