using System.Linq;
using System.Data.Entity;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSolutions.Common.Extensions;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ServicoContext
{
    public class ServicoRepository : IServicoRepository
    {

        public ServicoModel Find(long id)
        {
            using (Contexto db = new Contexto())
                return db.Servicos.AsNoTracking()
                    .SingleOrDefault(x => x.Id == id);
        }

        public void Save(ServicoModel model)
        {
            using (Contexto db = new Contexto())
            {

                if (model.Empresa.IsNotNull())
                    db.Empresas.Attach(model.Empresa);

                if (model.Funcionarios.IsNotNull())
                    model.Funcionarios.ToList().ForEach(f =>
                    {
                        if (f.IsNotNull())
                            db.Funcionarios.Attach(f);
                    });

                if (model.Id != 0)
                {
                    var entity = db.Servicos
                        .Include(x => x.Empresa)
                        .Include(x => x.Funcionarios)
                        .SingleOrDefault(c => c.Id == model.Id);

                    //Atribuições de propriedades simples
                    db.Entry(entity).CurrentValues.SetValues(model);

                    db.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    db.Servicos.Add(model);

                }

                db.SaveChanges();
            }
        }

        public void Delete(long id)
        {
            using (Contexto db = new Contexto())
            {
                ServicoModel s = db.Servicos
                    .Include(x => x.Empresa)
                    .Include(x => x.Funcionarios)
                    .SingleOrDefault(x => x.Id == id);

                db.Servicos.Remove(s);
                db.SaveChanges();
            }
        }
    }
}