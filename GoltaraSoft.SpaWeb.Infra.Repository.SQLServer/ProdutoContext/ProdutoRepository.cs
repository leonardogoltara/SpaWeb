using System.Linq;
using System.Data.Entity;
using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;
using GoltaraSolutions.Common.Extensions;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.ProdutoContext
{
    public class ProdutoRepository : IProdutoRepository
    {
        public ProdutoModel Find(long id)
        {
            using (Contexto db = new Contexto())
                return db.Produtos
                    .Include(x => x.Empresa)
                    .SingleOrDefault(x => x.Id == id);
        }
        
        public void Save(ProdutoModel model)
        {
            using (Contexto db = new Contexto())
            {
                if (model.Empresa.IsNotNull())
                    db.Empresas.Attach(model.Empresa);
                
                if (model.Id != 0)
                {
                    var entity = db.Produtos
                        .Include(x => x.Empresa)
                        .SingleOrDefault(c => c.Id == model.Id);

                    db.Entry(entity).CurrentValues.SetValues(model);
                    db.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    db.Produtos.Add(model);
                }

                db.SaveChanges();
            }
        }
        
        public void Delete(long id)
        {
            using (Contexto db = new Contexto())
            {
                ProdutoModel s = db.Produtos
                    .Include(x => x.Empresa)
                    .SingleOrDefault(x => x.Id == id);

                db.Produtos.Remove(s);
                db.SaveChanges();
            }
        }
    }
}
