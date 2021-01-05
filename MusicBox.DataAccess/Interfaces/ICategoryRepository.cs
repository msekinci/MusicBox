using MusicBox.Models.DbModels;

namespace MusicBox.DataAccess.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
