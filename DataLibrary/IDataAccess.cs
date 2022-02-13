using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace DataLibrary
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string i_Sql, U i_Params, string i_ConnectionString);

        Task<T> LoadItem<T, U>(string i_Sql, U i_Params, string i_ConnectionString);

        Task SaveData<T>(string i_Sql, T i_Params, string i_ConnectionString);

    }
}