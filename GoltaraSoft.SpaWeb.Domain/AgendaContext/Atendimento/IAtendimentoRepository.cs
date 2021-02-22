using GoltaraSolutions.Common.Domain.Repository;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento
{
    public interface IAtendimentoRepository : IRepository<AtendimentoModel>
    {
        AtendimentoModel GetAtendimento(long idCliente, long idServico, long idFuncionario, DateTime DataHora);
   }
}