using System;

namespace MusicBox.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository category { get; }
        ICoverTypeRepository coverType { get; }
        ISPCallRepository spCall { get; }
        void Save();
    }
}
