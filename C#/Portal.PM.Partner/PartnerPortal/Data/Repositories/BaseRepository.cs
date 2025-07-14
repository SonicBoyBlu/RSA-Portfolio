using Acme.Data.Maps;
using Acme.Models;
using Dapper.FluentMap;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Acme.Data.Repositories
{
    public class BaseRepository
    {
        private static bool initialized = false;
        public BaseRepository()
        {
            Initialize();
        }

        public static void Initialize()
        {
            if (!initialized)
            {
                initialized = true;
                FluentMapper.Initialize(config =>
                {                    
                    config.AddMap(new TaskSummaryMap());
                    config.AddMap(new TaskCostMap());
                    config.AddMap(new CrmPhoneNumberMap());
                });
            }
        }
        virtual protected string ConnectionString
        {
            get
            {
                return GlobalSettings.DateStores.Breezeway;
            }
        }

        protected async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync(); // Asynchronously open a connection to the database
                    return await getData(connection); // Asynchronously execute getData, which has been passed in as a Func<IDBConnection, Task<T>>
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        protected T WithConnection<T>(Func<IDbConnection, T> getData)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open(); // Open a connection to the database
                    return getData(connection); // Execute getData, which has been passed in as a Func<IDBConnection, Task<T>>
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        protected void AddPageSortSQL(string query, PageSortFilter filter)
        {
            if(!string.IsNullOrWhiteSpace(filter.SortField))
            {
                query += $" ORDER BY {filter.SortField}";
            }
            if (!filter.SortAscending)
            {
                query += " DESC";
            }
            query += $" OFFSET {filter.PageSize} * ({filter.PageNumber}) -1 ROWS";
            query += $" FETCH NEXT {filter.PageSize} ROWS ONLY OPTION(RECOMPILE)";
        }
    }


}
