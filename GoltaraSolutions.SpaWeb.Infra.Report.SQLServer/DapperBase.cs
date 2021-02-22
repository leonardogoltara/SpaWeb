using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GoltaraSolutions.SpaWeb.Infra.Report.SQLServer
{
    public abstract class DapperBase
    {
        //private int _commandTimeout = 300;
        string _connectionString;

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
