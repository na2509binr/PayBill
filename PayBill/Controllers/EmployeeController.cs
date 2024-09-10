using Microsoft.AspNetCore.Mvc;
using PayBill.Data;
using PayBill.Handle.OracleDB;
using PayBill.Models;
using X.PagedList;

namespace PayBill.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly PayBillDbContext _dbContext;
        clsDb _clsDb;

        public EmployeeController(ILogger<EmployeeController> logger, PayBillDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            _clsDb = new clsDb();
            Dish[] arrDishes = _dbContext.Dishes.Where(c => c.Is_For_Employee == 1).ToArray();
            Employee[] arrEmployee = _dbContext.Employees.ToArray();

            GetDataToViewModel getDataModel = new GetDataToViewModel();
            getDataModel.employeeList = arrEmployee.ToPagedList<Employee>();
            getDataModel.dishesList = arrDishes.ToPagedList<Dish>();
            getDataModel.message = TempData["Message"] as string;

            return View("Index", getDataModel);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Employees.Add(collection);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            else { return View(); }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
