using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System;

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
    }
}
