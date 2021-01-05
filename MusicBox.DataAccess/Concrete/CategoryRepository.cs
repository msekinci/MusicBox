﻿using MusicBox.DataAccess.Data;
using MusicBox.DataAccess.Interfaces;
using MusicBox.Models.DbModels;
using System.Linq;

namespace MusicBox.DataAccess.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var data = _db.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (data != null)
            {
                data.CategoryName = category.CategoryName;
            }
            _db.SaveChanges();
        }
    }
}
