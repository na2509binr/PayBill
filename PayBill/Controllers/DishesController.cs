using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayBill.Data;
using PayBill.Models;

namespace PayBill.Controllers
{
    public class DishesController : Controller
    {
        private readonly PayBillDbContext _context;

        public DishesController(PayBillDbContext context)
        {
            _context = context;
        }
        // GET: DishesController
        public ActionResult Index()
        {
            var dishes = _context.Dishes.ToList();
            return View(dishes);
        }

        // GET: DishesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DishesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DishesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dish collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Dishes.Add(collection);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            else { return View(); }
        }

        // GET: DishesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DishesController/Edit/5
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

        // GET: DishesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DishesController/Delete/5
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
