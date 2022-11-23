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
    public class Dapper : IDapper
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "OnlineMusicDbContext";

        public Dapper(IConfiguration config)
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
        }

        public async Task<int> CheckCode<T>(string code, string nameEntity)
        {
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            var queryCmd = "select Id from " + nameEntity + " where Code = @code";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@code", code);
            Log.Information("Dapper query: " + queryCmd);

            var res = await connection.QueryAsync<T>(queryCmd, parameter, commandType: CommandType.Text);
            return res.Count();
        }

        /// <summary>
        /// Kiểm tra bộ khóa đã tồn tại trong bảng dữ liệu hay chưa
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="LstParams"></param>
        /// <param name="nameEntity"></param>
        /// <returns></returns>
        public async Task<int> CheckCode<T>(List<DapperParamsQueryCommand> LstParams, string nameEntity)
        {
            StringBuilder sBuider = new StringBuilder();
            if (LstParams.Count() > 0)
            {
                sBuider.Append("WHERE ");
                foreach (var item in LstParams)
                {
                    if (string.IsNullOrEmpty(item.SqlOperator))
                    {
                        sBuider.Append(string.Format(" {0} [{1}] {2} {3}", item.SqlOperator, item.FieldName, item.Operator, item.ValueCompare));
                    }
                    else
                    {
                        sBuider.Append(string.Format(" [{0}] {1} {2}", item.FieldName, item.Operator, item.ValueCompare));
                    }
                }
            }
            using var connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            string sSQL = string.Format("SELECT TOP 1 * FROM {0} (NOLOCK) {1}", nameEntity, sBuider.ToString());
            Log.Information("Dapper query: " + sSQL);

            var res = await connection.QueryAsync<T>(sSQL, commandType: CommandType.Text);
            return res.Count();
        }
    }
}
