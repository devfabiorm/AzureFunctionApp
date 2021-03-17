using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Empresa.Projeto_Demanda
{
    public class AzureSqlRepository
    {
        private readonly string _connAzureSql;
        private SqlConnection _sqlConnection;

        public AzureSqlRepository()
        {
            _connAzureSql = Environment.GetEnvironmentVariable("AzureSQL_ConnectionString");
        }

        public void SaveDapper(UserSql userSql)
        {
            new Microsoft.Data.SqlClient.SqlConnection(_connAzureSql)
                .Insert(new UserSql { Name = userSql.Name });
        }

        public void SaveADO(UserSql userSql)
        {
            using(var command = _sqlConnection.CreateCommand())
            {
                command.Connection = new SqlConnection(_connAzureSql);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "INSERT INTO UserSqls (Name) values ('@Name')";

                var parameterName = command.CreateParameter();
                parameterName.ParameterName = nameof(userSql.Name);
                parameterName.Value = userSql.Name;
                command.Parameters.Add(parameterName);

                if (command.Connection.State != System.Data.ConnectionState.Open)
                    command.Connection.Open();

                command.ExecuteNonQuery();

                command.Connection.Close();
            }
        }

        public IEnumerable<UserSql> GetAll()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(_connAzureSql)
                .Query<UserSql>("SELECT * FROM dbo.UserSqls ORDER BY Id Desc");
        }
    }
}
