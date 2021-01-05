using Dapper;
using System;
using System.Collections.Generic;

namespace MusicBox.DataAccess.Interfaces
{
    public interface ISPCallRepository : IDisposable
    {
        T Single<T>(string prosedureName, DynamicParameters parameters = null);
        void Execute(string prosedureName, DynamicParameters parameters = null);
        T OneRecord<T>(string prosedureName, DynamicParameters parameters = null);
        IEnumerable<T> List<T>(string prosedureName, DynamicParameters parameters = null);
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string prosedureName, DynamicParameters parameters = null);
    }
}
