using System;

namespace MusicBox.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository category { get; }
        ISPCallRepository spCall { get; }
        void Save();
    }
}
