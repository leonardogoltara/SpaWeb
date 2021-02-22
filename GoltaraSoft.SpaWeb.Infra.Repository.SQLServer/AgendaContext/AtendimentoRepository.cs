using System;
using System.Linq;
using System.Data.Entity;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using GoltaraSolutions.Common.Extensions;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer.AgendaContext
{
    public class AtendimentoRepository : IAtendimentoRepository
    {
        public AtendimentoRepository()
        {
        }

        public AtendimentoModel GetAtendimento(long idCliente, long idServico, long idFuncionario, DateTime DataHora)
        {
            using (Contexto db = new Contexto())
                return db.Atendimentos
                    .Include(x => x.Cliente)
                    .Include(x => x.Servico)
                    .Include(x => x.Funcionario)
                    .SingleOrDefault(x => x.IdCliente == idCliente
                    && x.IdServico == idServico
                    && x.IdFuncionario == idFuncionario
                    && x.DataHora == DataHora);
        }

        public AtendimentoModel Find(long id)
        {
            using (Contexto db = new Contexto())
                return db.Atendimentos
                    .Include(x => x.Cliente)
                    .Include(x => x.Servico)
                    .Include(x => x.Funcionario)
                    .SingleOrDefault(x => x.Id == id);

        }

        public void Save(AtendimentoModel model)
        {
            using (Contexto db = new Contexto())
            {
                if (model.Empresa.IsNotNull())
                    db.Empresas.Attach(model.Empresa);

                if (model.Cliente.IsNotNull())
                    db.Clientes.Attach(model.Cliente);

                if (model.Servico.IsNotNull())
                    db.Servicos.Attach(model.Servico);

                if (model.Funcionario.IsNotNull())
                    db.Funcionarios.Attach(model.Funcionario);

                if (model.Id != 0)
                {
                    var entity = db.Atendimentos
                        .Include(x => x.Empresa)
                        .Include(x => x.Funcionario)
                        .Include(x => x.Servico)
                        .Include(x => x.Cliente)
                        .SingleOrDefault(c => c.Id == model.Id);

                    db.Entry(entity).CurrentValues.SetValues(model);
                    db.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    db.Atendimentos.Add(model);
                }

                db.SaveChanges();
            }
        }

        public void Delete(long id)
        {
            using (Contexto db = new Contexto())
            {
                AtendimentoModel s = db.Atendimentos
                    .Include(x => x.Empresa)
                    .Include(x => x.Funcionario)
                    .Include(x => x.Servico)
                    .Include(x => x.Cliente)
                    .SingleOrDefault(c => c.Id == id);

                db.Atendimentos.Remove(s);
                db.SaveChanges();
            }
        }



    }
}