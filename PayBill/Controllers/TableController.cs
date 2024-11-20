using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayBill.Data;
using PayBill.Models;
using System.Diagnostics;
using X.PagedList;

namespace PayBill.Controllers
{
    public class TableController : Controller
    {
        private readonly PayBillDbContext _dbContext;

        public TableController(PayBillDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // GET: TableController
        public ActionResult Index()
        {
            var dishes = _dbContext.Dishes.ToArray();
            var tables = _dbContext.Tables.ToArray();

            GetDataToViewModel getDataModel = new GetDataToViewModel();
            getDataModel.tablesList = tables.ToPagedList<Tables>();
            getDataModel.dishesList = dishes.ToPagedList<Dish>();
            getDataModel.message = TempData["Message"] as string;

            return View("Index", getDataModel);
        }

        // GET: TableController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TableController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TableController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tables table)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Tables.Add(table);
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

        // GET: TableController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TableController/Edit/5
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

        // GET: TableController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TableController/Delete/5
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

        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "sa123")
            {
                return RedirectToAction("Index", "Table");
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
