using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Music3_Core.Entities.BaseEntity;
using Music3_Core.Extension.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Music3_Core.QueryCommand;

namespace Music3_Core.Extension.Repositories
{
    public class DapperExtension : IDapperExtension
    {
        private readonly IConfiguration _config;
        private readonly string Connectionstring = "OnlineMusicDbContext";

        public DapperExtension(IConfiguration config)
        {
            _config = config;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<T> GetAyncFirst<T>(string queryCmd, DynamicParameters parms, CommandType commandType)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            Log.Information("Dapper query: " + queryCmd);
            return await connection.QueryFirstOrDefaultAsync<T>(queryCmd, parms, commandType: commandType);
        }


        public async Task<IEnumerable<T>> GetAllAync<T>(string queryCmd, DynamicParameters parms, CommandType commandType)
        {
            Log.Information("Dapper query: " + queryCmd);
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await connection.QueryAsync<T>(queryCmd, parms, commandType: commandType);
        }
        public async Task<IEnumerable<T>> GetList<T>(string queryCmd, DynamicParameters parms, CommandType commandType)
        {
            Log.Information("Dapper query: " + queryCmd);

            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            return await connection.QueryAsync<T>(queryCmd, parms, commandType: commandType);
        }

        public async Task<IEnumerable<T>> GetListByListId<T>(IEnumerable<string> listId, string nameEntity, CommandType commandType)
        {

            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            var queryCmd = "select * from " + nameEntity + " where " + nameof(BaseEntity.Id) + " in @ids";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@ids", listId);
            Log.Information("Dapper query: " + queryCmd);

            return await connection.QueryAsync<T>(queryCmd, parameter, commandType: commandType);
        }
        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }    }
}
