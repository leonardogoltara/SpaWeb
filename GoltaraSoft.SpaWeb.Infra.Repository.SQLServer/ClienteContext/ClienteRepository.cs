using System.Linq;
using System.Data.Entity;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.Common.Extensions;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ClienteContext
{
    public class ClienteRepository : IClienteRepository
    {
        public ClienteModel Find(long id)
        {
            using (Contexto db = new Contexto())
                return db.Clientes
                    .SingleOrDefault(x => x.Id == id);
        }

        public void Save(ClienteModel model)
        {
            using (Contexto db = new Contexto())
            {
                if (model.Empresa.IsNotNull())
                    db.Empresas.Attach(model.Empresa);

                if (model.Origem.IsNotNull())
                    db.Origens.Attach(model.Origem);

                if (model.Id != 0)
                {
                    var entity = db.Clientes
                        .Include(x => x.Empresa)
                        .Include(x => x.Origem)
                        .SingleOrDefault(c => c.Id == model.Id);

                    db.Entry(entity).CurrentValues.SetValues(model);
                    db.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    db.Clientes.Add(model);
                }

                db.SaveChanges();
            }
        }

        public void Delete(long id)
        {
            using (Contexto db = new Contexto())
            {
                ClienteModel s = db.Clientes
                    .Include(x => x.Origem)
                    .Include(x => x.Empresa)
                    .SingleOrDefault(x => x.Id == id);
                db.Clientes.Remove(s);
                db.SaveChanges();
            }
        }
    }
}
