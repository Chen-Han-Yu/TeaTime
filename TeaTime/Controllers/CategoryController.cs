using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TeaTime.Data;
using TeaTime.Models;

namespace TeaTime.Controllers
{
    public class CategoryController : Controller
    {
        //將資料庫內的資料抓出來顯示
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //為了將資料顯示出來需要使用Model的Category
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        //透過UI介面新增資料到資料庫
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //檢查類別名稱不能跟顯示順序一致
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "類別名稱不能跟顯示順序一致");
            }
            // 檢查顯示順序是否重複
            if (_db.Categories.Any(c => c.DisplayOrder == obj.DisplayOrder))
            {
                ModelState.AddModelError("DisplayOrder", "顯示順序重複，請輸入其他數字");
            }
            //ModelState.IsValid可以檢查資料是否正確
            if (ModelState.IsValid)
            {
                //如果資料正確就將資料寫入資料庫
                _db.Categories.Add(obj);
                _db.SaveChanges(); //上面為每個物件都新增，需要寫入要加SaveChanges
                //新增TempData可以進行畫面刷新顯示
                TempData["success"] = "類別新增成功";
                return RedirectToAction("Index");
            }
            return View();
        }
        //新增可以編輯功能
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditPOST(Category obj)
        {
            // 檢查顯示順序是否重複（排除自己）
            if (_db.Categories.Any(c => c.DisplayOrder == obj.DisplayOrder && c.Id != obj.Id))
            {
                ModelState.AddModelError("DisplayOrder", "顯示順序重複，請輸入其他數字");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                //新增TempData可以進行畫面刷新顯示
                TempData["success"] = "編輯完成！";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //新增可以刪除功能
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            //新增TempData可以進行畫面刷新顯示
            TempData["success"] = "刪除成功！";
            return RedirectToAction("Index");

        }

    }
}
