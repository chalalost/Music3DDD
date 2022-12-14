using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Music3_Core.Extension.IRepositories
{
    public interface IDapperExtension : IDisposable
    {
        DbConnection GetDbconnection();

        Task<T> GetAyncFirst<T>(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure);

        Task<IEnumerable<T>> GetList<T>(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure);

        Task<IEnumerable<T>> GetAllAync<T>(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure);

        Task<IEnumerable<T>> GetListByListId<T>(IEnumerable<string> listId, string nameEntity, CommandType commandType);

    }
}
