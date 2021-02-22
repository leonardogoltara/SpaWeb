using GoltaraSolutions.Common.Domain;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System;

namespace GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento
{
    public class AtendimentoModel : Aggregate
    {
        public DateTime DataHora { get; private set; }
        public long IdCliente { get; private set; }
        public long IdServico { get; private set; }
        public long IdFuncionario { get; private set; }
        public ClienteModel Cliente { get; private set; }
        public ServicoModel Servico { get; private set; }
        public FuncionarioModel Funcionario { get; private set; }
        public string GuidUsuarioAgendou { get; private set; }
        public decimal Valor { get; set; }
        public bool Cancelado { get; private set; }
        public bool Concluido { get; private set; }
        public bool Confirmado { get; private set; }
        public EmpresaModel Empresa { get; private set; }
        public long IdEmpresa { get; private set; }

        public AtendimentoModel()
        {
            Confirmado = true;
            Concluido = false;
            Cancelado = false;
            DataHora = DateTime.Now;
        }
        public AtendimentoModel(EmpresaModel empresa, long id, DateTime datahora, ClienteModel cliente, ServicoModel servico, FuncionarioModel funcionario, string guidUsuarioAgendou, decimal precoServico)
            : this(empresa, datahora, cliente, servico, funcionario, guidUsuarioAgendou, precoServico)
        {
            Id = id;
        }
        public AtendimentoModel(EmpresaModel empresa, DateTime datahora, ClienteModel cliente, ServicoModel servico, FuncionarioModel funcionario, string guidUsuarioAgendou, decimal precoServico) : this()
        {
            if (empresa.IsNull())
                throw new AtendimentoInvalidoException("Empresa inválida.");

            Fill(datahora, cliente, servico, funcionario, guidUsuarioAgendou, precoServico);
            Confirmado = true;

            Empresa = null;
            IdEmpresa = empresa.Id;
        }
        public void PreAtendimento(EmpresaModel empresa, DateTime datahora, ClienteModel cliente, ServicoModel servico, FuncionarioModel funcionario, string guidUsuarioAgendou, decimal precoServico)
        {
            if (empresa.IsNull())
                throw new AtendimentoInvalidoException("Empresa inválida.");

            Fill(datahora, cliente, servico, funcionario, guidUsuarioAgendou, precoServico);
            Confirmado = false;

            Empresa = null;
            IdEmpresa = empresa.Id;
        }

        internal void Editar(DateTime datahora, ClienteModel cliente, ServicoModel servico, FuncionarioModel funcionario, string guidUsuarioAgendou, decimal precoServico)
        {
            Fill(datahora, cliente, servico, funcionario, guidUsuarioAgendou, precoServico);
        }
        internal void Cancelar()
        {
            if (Cancelado)
                throw new AtendimentoInvalidoException("Não é possível cancelar um atendimento cancelado.");
            if (Concluido)
                throw new AtendimentoInvalidoException("Não é possível cancelar um atendimento concluido.");

            Cancelado = true;
        }
        internal void Concluir(decimal precoServico)
        {
            if (Cancelado)
                throw new AtendimentoInvalidoException("Não é possível concluir um atendimento cancelado.");
            if (Concluido)
                throw new AtendimentoInvalidoException("Não é possível concluir um atendimento concluido.");

            if (Servico.Preco != precoServico)
            {
                if (Servico.PrecoFixo)
                    throw new AtendimentoInvalidoException("Atendimentos de serviço com preço fixo não pode ter o preço alterado.");

                Valor = precoServico;
            }

            Concluido = true;
        }
        internal void Deletar()
        {
            Deletado = true;
        }
        internal void Fill(DateTime datahora, ClienteModel cliente, ServicoModel servico, FuncionarioModel funcionario, string guidUsuarioAgendou, decimal precoServico)
        {
            if (datahora.IsNull()) { throw new AtendimentoInvalidoException("Data inválida."); }
            if (cliente.IsNull()) { throw new AtendimentoInvalidoException("Cliente inválido."); }
            if (servico.IsNull()) { throw new AtendimentoInvalidoException("Serviço inválido."); }
            if (funcionario.IsNull()) { throw new AtendimentoInvalidoException("Funcionário inválido."); }
            if (guidUsuarioAgendou.IsNull()) { throw new AtendimentoInvalidoException("Usuário que agendou inválido."); }
            if (precoServico <= 0) { throw new AtendimentoInvalidoException("Um Atendimento não pode ter valor zero."); }

            DataHora = datahora;
            Cliente = null;
            IdCliente = cliente.Id;
            Servico = null;
            IdServico = servico.Id;
            Funcionario = null;
            IdFuncionario = funcionario.Id;
            GuidUsuarioAgendou = guidUsuarioAgendou;
            Valor = precoServico;
        }

        public override bool Equals(object obj)
        {
            var other = obj as AtendimentoModel;
            if (other.IsNull()) return false;

            return Id == other.Id;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
