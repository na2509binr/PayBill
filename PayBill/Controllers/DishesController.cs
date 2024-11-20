using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayBill.Data;
using PayBill.Models;
using ZstdSharp.Unsafe;

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

        public IActionResult Edit(int id)
        {
            var dish = _context.Dishes.Find(id);  // Lấy món ăn từ dịch vụ.
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);  // Trả về view với mô hình Dish.
        }

        // POST: DishesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Dish updatedDish)
        {
            if (id != updatedDish.ID_Dish)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Dishes.Update(updatedDish);  // Cập nhật món ăn trong cơ sở dữ liệu.
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));  // Sau khi sửa xong, chuyển về trang Index.
                }
                catch (Exception)
                {
                    // Handle errors (ví dụ như khi có lỗi cập nhật).
                    return View(updatedDish);
                }
            }
            return View(updatedDish);
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
