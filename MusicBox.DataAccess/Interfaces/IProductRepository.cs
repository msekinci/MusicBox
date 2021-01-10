using MusicBox.Models.DbModels;

namespace MusicBox.DataAccess.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
