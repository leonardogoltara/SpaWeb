using Dapper;
using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Domain.Repository;
using System;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.DapperSQLServer
{
    /// <summary>
    /// Referência - https://github.com/ericdc1/Dapper.SimpleCRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleCRUD<T> : DapperBase, IRepository<T> where T : Aggregate
    {
        protected readonly int _commandTimeout = 300000;

        public SimpleCRUD() : base()
        {
            var resolver = new CustomResolver();
            SimpleCRUD.SetTableNameResolver(resolver);
        }

        public virtual void Delete(long id)
        {
            using (var sqlConnection = Connection())
            {
                var o = Activator.CreateInstance<T>();
                o.Id = id;

                sqlConnection.Delete(o, null, _commandTimeout);
            }
        }

        public virtual T Find(long id)
        {
            using (var sqlConnection = Connection())
            {
                return sqlConnection.Get<T>(id, null, _commandTimeout);
            }
        }

        public virtual void Save(T model)
        {
            using (var sqlConnection = Connection())
            {
                if (sqlConnection.Get<T>(model.Id, null, _commandTimeout) == null)
                {
                    long? id = sqlConnection.Insert(model, null, _commandTimeout);
                    if (id.HasValue)
                        model.Id = id.Value;
                }
                else { 
                    sqlConnection.Update(model, null, _commandTimeout);
                }
            }
        }

        public class CustomResolver : Dapper.SimpleCRUD.ITableNameResolver //, SimpleCRUD.IColumnNameResolver
        {
            public string ResolveTableName(Type type)
            {
                var schema = type.FullName.Split('.')[type.FullName.Split('.').Length - 2].Replace("Context", "");
                var table = type.Name?.Replace("Model", "").Replace("Models", "");
                return string.Format("{0}.{1}", schema, table);
            }

            //public string ResolveColumnName(PropertyInfo propertyInfo)
            //{
            //    return string.Format("{0}_{1}", propertyInfo.DeclaringType.Name, propertyInfo.Name);
            //}
        }
    }
}
