using MusicBox.DataAccess.Data;
using MusicBox.DataAccess.Interfaces;

namespace MusicBox.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            category = new CategoryRepository(_db);
            product = new ProductRepository(_db);
            coverType = new CoverTypeRepository(_db);
            spCall = new SPCallRepository(_db);
        }
        public ICategoryRepository category { get; private set; }
        public ICoverTypeRepository coverType { get; private set; }
        public IProductRepository product { get; private set; }
        public ISPCallRepository spCall { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
