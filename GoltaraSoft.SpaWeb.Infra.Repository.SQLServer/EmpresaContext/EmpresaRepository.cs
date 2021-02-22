using System.Linq;
using System.Data.Entity;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.EmpresaContext
{
    public class EmpresaRepository : IEmpresaRepository
    {
        public EmpresaRepository()
        {
            Contexto db = new Contexto();
            db.Dispose();
        }

        public EmpresaModel Find(long id)
        {
            using (Contexto db = new Contexto())
                return db.Empresas
                    .Include(e => e.ResponsavelCobranca)
                    //.Include(e => e.Funcionarios)
                    //.Include(e => e.Atendimentos)
                    //.Include(e => e.Clientes)
                    //.Include(e => e.Origens)
                    //.Include(e => e.Produtos)
                    //.Include(e => e.Servicos)
                    .SingleOrDefault(x => x.Id == id);
        }

        public EmpresaModel FindIncludingAll(long id)
        {
            using (Contexto db = new Contexto())
                return db.Empresas
                    .Include(e => e.ResponsavelCobranca)
                    .SingleOrDefault(x => x.Id == id);
        }

        public EmpresaModel Find(string cnpj)
        {
            using (Contexto db = new Contexto())
                return db.Empresas
                        .Include(e => e.ResponsavelCobranca)
                        //.Include(e => e.Funcionarios)
                        //.Include(e => e.Atendimentos)
                        //.Include(e => e.Clientes)
                        //.Include(e => e.Origens)
                        //.Include(e => e.Produtos)
                        //.Include(e => e.Servicos)
                        .SingleOrDefault(x => x.CNPJ == cnpj);
        }

        public void Save(EmpresaModel model)
        {
            using (Contexto db = new Contexto())
            {
                if (model.Id != 0)
                {
                    var entity = db.Empresas
                        .Include(e => e.ResponsavelCobranca)
                        //.Include(e => e.Funcionarios)
                        //.Include(e => e.Atendimentos)
                        //.Include(e => e.Clientes)
                        //.Include(e => e.Origens)
                        //.Include(e => e.Produtos)
                        //.Include(e => e.Servicos)
                        .SingleOrDefault(c => c.Id == model.Id);

                    db.Entry(entity).CurrentValues.SetValues(model);

                    //-- Responsavel -- //
                    entity.ResponsavelCobranca.Editar(model.ResponsavelCobranca.Nome,
                        model.ResponsavelCobranca.Telefone,
                        model.ResponsavelCobranca.Email);
                    db.Entry(entity.ResponsavelCobranca).State = EntityState.Modified;
                    //-- Responsavel -- \\

                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Empresas.Add(model);
                    db.SaveChanges();
                }
            }
        }

        public void Delete(long id)
        {
            using (Contexto db = new Contexto())
            {
                EmpresaModel s = db.Empresas
                    .Include(e => e.ResponsavelCobranca)
                    .SingleOrDefault(x => x.Id == id);

                db.Empresas.Remove(s);
                db.SaveChanges();
            }
        }

        public void PopularBancoTeste(EmpresaModel empresa)
        {
            using (Contexto db = new Contexto())
            {
                Banco.Seed(empresa, db);
            }
        }
    }
}
