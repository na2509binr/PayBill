using Microsoft.AspNetCore.Mvc;
using PayBill.Data;
using PayBill.DTOs;
using PayBill.Models;
using PayBill.Services;
using X.PagedList;

namespace PayBill.Controllers;

public class HistoriesController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PayBillDbContext _dbContext;
    private readonly IDbServices _dbServices;

    public HistoriesController(ILogger<HomeController> logger, PayBillDbContext dbContext, IDbServices dbServices)
    {
        _logger = logger;
        _dbContext = dbContext;
        _dbServices = dbServices;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Histories(string fromdte, string todte)
    {
        try
        {
            Receipt[] receipts;
            receipts = _dbContext.Receipts.ToArray();
            var receiptsDetail = _dbContext.Receipt_Details.ToArray();
            var spending = _dbContext.Spendings.ToArray();
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
            getDataModel.SpendingModelList = spending.ToPagedList<Spending>();
            getDataModel.SpendingSummaryDtos = _dbServices.GetSpendingSummary().ToPagedList<SpendingSummaryDto>();
            getDataModel.ReceiptSummaryDtos = _dbServices.GetReceiptSummary().ToPagedList<ReceiptSummaryDto>();
            getDataModel.message = TempData["Message"] as string;
            return View("Histories", getDataModel);
        }
        catch (Exception)
        {
            return View("Histories");
        }
    }
}
