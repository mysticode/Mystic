/*
 * Copyright (C) 2012 The Mysticode Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
