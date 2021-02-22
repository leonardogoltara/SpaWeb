using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using GoltaraSolutions.SpaWeb.Infra.Repository.DapperSQLServer.ServicoContext;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.DapperSQLServer
{
    public abstract class DapperBase
    {
        //private int _commandTimeout = 300;
        readonly string _connectionString;

        protected DapperBase()
        {
            if (ConfigurationManager.ConnectionStrings["DefaultConnection"] == null)
                throw new Exception("ConnectionString não encontrada.");

            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        }

        protected SqlConnection Connection()
        {
            return new SqlConnection(_connectionString);
        }
    }

    public static class SqlServerExtensions
    {
        public static string Like(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
                return @"%" + input.Replace("[", "[[]").Replace("%", "[%]") + "%";

            return input;
        }
    }
}
