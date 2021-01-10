using MusicBox.DataAccess.Data;
using MusicBox.DataAccess.Interfaces;
using MusicBox.Models.DbModels;
using System;
using System.Linq;

namespace MusicBox.DataAccess.Concrete
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType coverType)
        {
            var data = _db.CoverTypes.FirstOrDefault(x => x.Id == coverType.Id);
            if (data != null)
            {
                data.Name = coverType.Name;
            }
        }
    }
}
