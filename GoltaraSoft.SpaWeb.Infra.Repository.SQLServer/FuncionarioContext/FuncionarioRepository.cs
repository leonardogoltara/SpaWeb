using System.Linq;
using System.Data.Entity;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.Common.Extensions;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.FuncionarioContext
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        public FuncionarioModel Find(long id)
        {
            using (Contexto db = new Contexto())
                return db.Funcionarios
                    .Include(x => x.Servicos)
                    .AsNoTracking()
                    .SingleOrDefault(x => x.Id == id);
        }

        public void Save(FuncionarioModel model)
        {
            using (Contexto db = new Contexto())
            {
                if (model.Empresa.IsNotNull())
                    db.Empresas.Attach(model.Empresa);

                if (model.Servicos.IsNotNull())
                    model.Servicos.ToList().ForEach(s =>
                    {
                        if (s.IsNotNull())
                        {
                            db.Servicos.Attach(s);
                        }
                    });

                if (model.Id != 0)
                {
                    var entity = db.Funcionarios
                        .Include(x => x.Empresa)
                        .Include(x => x.Servicos)
                        .SingleOrDefault(c => c.Id == model.Id);

                    if (entity.Servicos.IsNotNull())
                        entity.Servicos.Clear();

                    if (model.Servicos.IsNotNull())
                        model.Servicos.ToList().ForEach(x => entity.Servicos.Add(x));

                    db.Entry(entity).CurrentValues.SetValues(model);
                    db.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    db.Funcionarios.Add(model);
                }

                db.SaveChanges();
            }
        }

        public void Delete(long id)
        {
            using (Contexto db = new Contexto())
            {
                FuncionarioModel s = db.Funcionarios
                    .Include(x => x.Servicos)
                    .Include(x => x.Empresa)
                    .SingleOrDefault(x => x.Id == id);

                db.Funcionarios.Remove(s);
                db.SaveChanges();
            }
        }
    }
}
