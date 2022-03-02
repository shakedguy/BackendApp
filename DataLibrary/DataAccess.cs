using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace DataLibrary
{
    public class DataAccess : IDataAccess
    {

        public async Task<IEnumerable<T>> LoadData<T, U>(string i_Sql, U i_Params, string i_ConnectionString)
        {
            using (IDbConnection connection = new MySqlConnection(i_ConnectionString))
            {
                return await connection.QueryAsync<T>(i_Sql, i_Params);
            }
        }

        public async Task<T> LoadItem<T, U>(string i_Sql, U i_Params, string i_ConnectionString)
        {
            using (IDbConnection connection = new MySqlConnection(i_ConnectionString))
            {
                return await connection.QueryFirstAsync<T>(i_Sql, i_Params);
            }
        }

        public async Task SaveData<T>(string i_Sql, T i_Params, string i_ConnectionString)
        {
            using (IDbConnection connection = new MySqlConnection(i_ConnectionString))
            {
                await connection.ExecuteAsync(i_Sql, i_Params);
            }

        }
    }
}