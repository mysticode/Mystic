using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Mystic.DAL
{
    public static class SqlDbFactory
    {
        public static DbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        public static DbCommand CreateCommand(string storedProcedureName)
        {
            return new SqlCommand(storedProcedureName);
        }

        public static DbParameter CreateParameter(SqlDbType dbType)
        {
            return new SqlParameter
            {
                SqlDbType = dbType
            };
        }
    }
}
