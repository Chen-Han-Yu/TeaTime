using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaTime.DataAccess.Data;
using TeaTime.DataAccess.Repository.IRepository;
using TeaTime.Models;

namespace TeaTime.DataAccess.Repository
{
    //將CategoryRepository和ICategoryRepository的實作加入
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        //修改
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        //public void Save()
        //{
        //    _db.SaveChanges();
        //}
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
