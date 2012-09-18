using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;

using Mystic.Common.DTO;
using Mystic.Common.Parsers;

namespace Mystic.DAL
{
    public abstract class DalBase
    {
        private static string _connectionString;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                }
                return _connectionString;
            }
            protected set { _connectionString = value; }
        }

        public static DbConnection GetDbConnection()
        {
            return SqlDbFactory.CreateConnection(ConnectionString);
        }

        protected static DbParameter CreateNullParameter(string name, SqlDbType paramType)
        {
            var parameter = SqlDbFactory.CreateParameter(paramType);
            parameter.ParameterName = name;
            parameter.Value = null;
            parameter.Direction = ParameterDirection.Input;

            return parameter;
        }

        protected static DbParameter CreateNullParameter(string name, SqlDbType paramType, int size)
        {
            var parameter = SqlDbFactory.CreateParameter(paramType);
            parameter.ParameterName = name;
            parameter.Size = size;
            parameter.Value = null;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        protected static DbParameter CreateOutputParameter(string name, SqlDbType paramType)
        {
            var parameter = SqlDbFactory.CreateParameter(paramType);
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        protected static DbParameter CreateOutputParameter(string name, SqlDbType paramType, int size)
        {
            var parameter = SqlDbFactory.CreateParameter(paramType);
            parameter.Size = size;
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        protected static DbParameter CreateParameter(string name, Guid? value)
        {
            if (!value.HasValue)
            {
                return CreateNullParameter(name, SqlDbType.UniqueIdentifier);
            }
            else
            {
                var parameter = SqlDbFactory.CreateParameter(SqlDbType.UniqueIdentifier);
                parameter.ParameterName = name;
                parameter.Value = value.Value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        protected static DbParameter CreateParameter(string name, int? value)
        {
            if (!value.HasValue)
            {
                return CreateNullParameter(name, SqlDbType.Int);
            }
            else
            {
                var parameter = SqlDbFactory.CreateParameter(SqlDbType.Int);
                parameter.ParameterName = name;
                parameter.Value = value.Value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        protected static DbParameter CreateParameter(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return CreateNullParameter(name, SqlDbType.NVarChar);
            }
            else
            {
                var parameter = SqlDbFactory.CreateParameter(SqlDbType.NVarChar);
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.Direction = ParameterDirection.Input;
                return parameter;
            }
        }

        protected static DbCommand GetDbSprocCommand(string storedProcedureName)
        {
            var command = SqlDbFactory.CreateCommand(storedProcedureName);
            command.Connection = GetDbConnection();
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        protected static T GetSingle<T>(ref DbCommand command) where T : DTOBase
        {
            try
            {
                command.Connection.Open();
                var reader = command.ExecuteReader();
                try
                {
                    if (reader.HasRows && reader.Read())
                    {
                        var parser = DTOParserFactory.GetParserOf<T>(reader);
                        return (T)parser.PopulateDTO(reader);
                    }
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                        ((IDisposable)reader).Dispose();
                    }
                }
            }
            finally
            {
                if (command != null && command.Connection != null)
                {
                    command.Connection.Close();
                    ((IDisposable)command.Connection).Dispose();
                }
            }

            return null;
        }
        protected static List<T> GetDTOList<T>(ref DbCommand command) where T : DTOBase
        {
            List<T> dtoList = null;
            try
            {
                command.Connection.Open();
                var reader = command.ExecuteReader();
                try
                {
                    if (reader.HasRows)
                    {
                        var parser = DTOParserFactory.GetParserOf<T>(reader);
                        dtoList = new List<T>();
                        while (reader.Read())
                        {
                            T dto = null;
                            dto = (T)parser.PopulateDTO(reader);
                            dtoList.Add(dto);
                        }
                    }
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                        ((IDisposable)reader).Dispose();
                    }
                }
            }
            finally
            {
                if (command != null && command.Connection != null)
                {
                    command.Connection.Close();
                    ((IDisposable)command.Connection).Dispose();
                }
            }

            return dtoList;
        }
    }
}
