using System.Collections.Generic;
using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Infra.Repository;
using System.Linq;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.Common.Domain.Repository;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.InMemory
{
    public class GenericRepository<T>
        : HMRepository<T>, IRepository<T>
        where T : Aggregate
    {
        public override T Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.Id == (int)keyValues.Single());
        }
        public void Delete(long id)
        {
            T o = Find(id);
            Remove(o);
        }

        public ICollection<T> List(long idEmpresa)
        {
            return this.ToList();
        }

        public T Find(long id)
        {
            return this.SingleOrDefault(d => d.Id == id);
        }

        public void Save(T model)
        {
            if (Find(model.Id).IsNotNull()) Remove(model);
            Add(model);
        }

        public virtual T Find(long idEmpresa, string nome)
        {
            return null;
        }
    }
}
