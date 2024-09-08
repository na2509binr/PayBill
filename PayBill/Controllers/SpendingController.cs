using Microsoft.AspNetCore.Mvc;
using PayBill.Data;
using PayBill.Models;

namespace PayBill.Controllers
{
    public class SpendingController : Controller
    {
        private readonly PayBillDbContext _context;

        public SpendingController(PayBillDbContext context)
        {
            _context = context;
        }


        // GET: SpendingController
        public ActionResult Index()
        {
            return View("Spending");
        }

        // GET: SpendingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SpendingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpendingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Spending collection)
        {
            try
            {
                collection.Spending_ID = $"{DateTime.Now.ToString()}:{collection.SpendingType}";
                _context.Add(collection);
                _context.SaveChanges();
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SpendingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SpendingController/Edit/5
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

        // GET: SpendingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SpendingController/Delete/5
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
