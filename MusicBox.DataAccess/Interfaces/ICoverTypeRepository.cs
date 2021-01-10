using MusicBox.Models.DbModels;

namespace MusicBox.DataAccess.Interfaces
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType coverType);
    }
}
