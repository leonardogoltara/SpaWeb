using System.Linq;
using System.Data.Entity;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.Common.Extensions;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ClienteContext
{
    public class OrigemRepository : IOrigemRepository
    {

        public OrigemModel Find(long id)
        {
            using (Contexto db = new Contexto())
                return db.Origens
                    .Include(x => x.Empresa)
                    .SingleOrDefault(x => x.Id == id);
        }

        public void Save(OrigemModel model)
        {
            using (Contexto db = new Contexto())
            {
                if (model.Empresa.IsNotNull())
                    db.Empresas.Attach(model.Empresa);

                if (model.Id != 0)
                {
                    var entity = db.Origens
                        .Include(x => x.Empresa)
                        .SingleOrDefault(c => c.Id == model.Id);

                    db.Entry(entity).CurrentValues.SetValues(model);
                    db.Entry(entity).State = EntityState.Modified;

                }
                else
                {
                    db.Origens.Add(model);
                }

                db.SaveChanges();
            }
        }

        public void Delete(long id)
        {
            using (Contexto db = new Contexto())
            {
                OrigemModel s = db.Origens
                    .Include(x => x.Empresa)
                    .SingleOrDefault(x => x.Id == id);
                db.Origens.Remove(s);
                db.SaveChanges();
            }
        }
    }
}
