using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System;
using System.Collections.Generic;
using System.Linq;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento.ReportViews;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext.ReportViews;
using GoltaraSolutions.Common.Domain.Exceptions;

namespace GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento
{
    public sealed class AtendimentoDomainServices
    {
        IAtendimentoRepository _repository = null;
        IAtendimentoReport _report = null;
        EmpresaDomainServices svEmpresa = null;
        ClienteDomainServices svCliente = null;
        FuncionarioDomainServices svFuncionario = null;
        ServicoDomainServices svServico = null;

        public AtendimentoDomainServices(IAtendimentoRepository repository,
            IAtendimentoReport report,
            EmpresaDomainServices empresaDomainServices,
            ClienteDomainServices clienteDomainServices,
            FuncionarioDomainServices funcionarioDomainServices,
            ServicoDomainServices servicoDomainServices)
        {
            _repository = repository;
            _report = report;

            svEmpresa = empresaDomainServices;
            svCliente = clienteDomainServices;
            svFuncionario = funcionarioDomainServices;
            svServico = servicoDomainServices;
        }

        public void Agendar(long idEmpresa, DateTime data, long idServico, long idCliente, long idFuncionario, string guidUsuarioAgendou)
        {
            try
            {
                //if (data < DateTime.Now.FirstHourOfDay().AddMonths(-1))
                //    { throw new AtendimentoInvalidoException("Data inválido."); }

                //if (data.DateHourMinute() < DateTime.Now.DateHourMinute())
                //{ throw new AtendimentoInvalidoException("Data inválida, não é possível realizar um agendamento para o passado."); }

                ClienteModel cliente = svCliente.Find(idCliente);
                if (cliente.IsNull()) { throw new AtendimentoInvalidoException("Cliente inválido."); }

                ServicoModel servico = svServico.Find(idServico);
                if (servico.IsNull()) { throw new AtendimentoInvalidoException("Serviço inválido."); }

                FuncionarioModel funcionario = svFuncionario.Find(idFuncionario);
                if (funcionario.IsNull()) { throw new AtendimentoInvalidoException("Funcionário inválido."); }

                AtendimentoModel agendamentoCadastrado = _repository.GetAtendimento(cliente.Id, servico.Id, funcionario.Id, data.DateHourMinute());
                if (agendamentoCadastrado.IsNotNull())
                    throw new AtendimentoInvalidoException("Já existe um atendimento para este cliente com este funcionário neste horário.");

                EmpresaModel empresa = svEmpresa.Find(idEmpresa);

                AtendimentoModel a = new AtendimentoModel(empresa, data.DateHourMinute(), cliente, servico, funcionario, guidUsuarioAgendou, servico.Preco);

                _repository.Save(a);
            }
            catch (DomainException dEx)
            {
                Logger.Log.Warn(dEx);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }

        public void Editar(long id, DateTime data, long idServico, long idCliente, long idFuncionario, string guidUsuarioAgendou)
        {
            try
            {
                ClienteModel cliente = svCliente.Find(idCliente);
                if (cliente.IsNull()) { throw new AtendimentoInvalidoException("Cliente inválido."); }

                ServicoModel servico = svServico.Find(idServico);
                if (servico.IsNull()) { throw new AtendimentoInvalidoException("Serviço inválido."); }

                FuncionarioModel funcionario = svFuncionario.Find(idFuncionario);
                if (funcionario.IsNull()) { throw new AtendimentoInvalidoException("Funcionário inválido."); }

                if (funcionario.Servicos.IsNotNull())
                {
                    if (!funcionario.Servicos.ToList().Exists(s => s.Id == servico.Id))
                    {
                        throw new AtendimentoInvalidoException("Este Funcionário não presta este serviço.");
                    }
                }

                AtendimentoModel agendamentoCadastrado = _repository.GetAtendimento(cliente.Id, servico.Id, funcionario.Id, data.DateHourMinute());
                if (agendamentoCadastrado.IsNotNull() && agendamentoCadastrado.Id != id)
                    throw new AtendimentoInvalidoException("Já existe um atendimento para este cliente com este funcionário neste horário.");

                AtendimentoModel a = _repository.Find(id);
                if (a.IsNull())
                    throw new AtendimentoInvalidoException($"Atendimento {id} não encontrado.");

                a.Editar(data.DateHourMinute(), cliente, servico, funcionario, guidUsuarioAgendou, servico.Preco);

                _repository.Save(a);
            }
            catch (DomainException dEx)
            {
                Logger.Log.Warn(dEx);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public void Cancelar(long id)
        {
            try
            {
                AtendimentoModel a = _repository.Find(id);
                if (a.IsNull())
                    throw new AtendimentoInvalidoException($"Atendimento {id} não encontrado.");

                a.Cancelar();
                _repository.Save(a);
            }
            catch (DomainException dEx)
            {
                Logger.Log.Warn(dEx);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }

        public void Concluir(long id)
        {
            Concluir(id, null);
        }

        public void Concluir(long id, decimal? valor)
        {
            try
            {
                AtendimentoModel a = _repository.Find(id);

                if (a.IsNull())
                    throw new AtendimentoInvalidoException($"Atendimento {id} não encontrado.");

                if (valor.IsNotNull())
                    a.Concluir(valor.Value);
                else
                    a.Concluir(a.Valor);

                _repository.Save(a);
            }
            catch (DomainException dEx)
            {
                Logger.Log.Warn(dEx);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public void Delete(long id)
        {
            try
            {
                AtendimentoModel a = _repository.Find(id);
                if (a.IsNull())
                    throw new AtendimentoInvalidoException($"Atendimento {id} não encontrado.");

                a.Deletar();
                _repository.Save(a);
            }
            catch (DomainException dEx)
            {
                Logger.Log.Warn(dEx);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public AtendimentoModel Find(long id)
        {
            try
            {
                return _repository.Find(id);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public AtendimentoModel Find(long idCliente, long idServico, long idFuncionario, DateTime DataHora)
        {
            try
            {
                return _repository.GetAtendimento(idCliente, idServico, idFuncionario, DataHora);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }

        public List<AtendimentoReportView> Relatorio(long idEmpresa, DateTime? DataInicial, DateTime? DataFinal, long? idCliente, long? idServico,
            long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            try
            {
                return _report.Relatorio(idEmpresa, DataInicial, DataFinal, idCliente, idServico, idFuncionario, idOrigem, Cancelado, Concluido);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }

        public List<AtendimentoReportAgrupadoView> RelatorioPorCliente(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico,
         long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            try
            {
                return _report.RelatorioPorCliente(idEmpresa, DataInicial, DataFinal, idCliente, idServico, idFuncionario, idOrigem, Cancelado, Concluido);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public List<AtendimentoReportAgrupadoView> RelatorioPorFuncionario(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico,
            long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            try
            {
                return _report.RelatorioPorFuncionario(idEmpresa, DataInicial, DataFinal, idCliente, idServico, idFuncionario, idOrigem, Cancelado, Concluido);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public List<AtendimentoReportAgrupadoView> RelatorioPorOrigem(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico,
            long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            try
            {
                return _report.RelatorioPorOrigem(idEmpresa, DataInicial, DataFinal, idCliente, idServico, idFuncionario, idOrigem, Cancelado, Concluido);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public List<AtendimentoReportAgrupadoView> RelatorioPorServico(long idEmpresa, DateTime DataInicial, DateTime DataFinal, long? idCliente, long? idServico,
            long? idFuncionario, long? idOrigem, bool? Cancelado, bool? Concluido)
        {
            try
            {
                return _report.RelatorioPorServico(idEmpresa, DataInicial, DataFinal, idCliente, idServico, idFuncionario, idOrigem, Cancelado, Concluido);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }

        public long IndicadorAtendimentosTodos(long idEmpresa)
        {
            try
            {
                return _report.Indicador(idEmpresa, DateTime.Now.AddDays(-7).FirstHourOfDay(), DateTime.Now.LastHourOfDay(),
                null, null, null, null, null);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public long IndicadorAtendimentosAbertos(long idEmpresa)
        {
            try
            {
                return _report.Indicador(idEmpresa, DateTime.Now.AddDays(-7).FirstHourOfDay(), DateTime.Now.LastHourOfDay(),
                null, null, null, false, false);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public long IndicadorAtendimentosCancelados(long idEmpresa)
        {
            try
            {
                return _report.Indicador(idEmpresa, DateTime.Now.AddDays(-7).FirstHourOfDay(), DateTime.Now.LastHourOfDay(),
                null, null, null, true, false);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public long IndicadorAtendimentosConcluidos(long idEmpresa)
        {
            try
            {
                return _report.Indicador(idEmpresa, DateTime.Now.AddDays(-7).FirstHourOfDay(), DateTime.Now.LastHourOfDay(),
                null, null, null, false, true);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }

        public List<TopCliente> Top10Clientes(long idEmpresa)
        {
            try
            {
                return _report.Top10Clientes(idEmpresa, DateTime.Now.AddDays(-30).FirstHourOfDay(),
                DateTime.Now.LastHourOfDay());
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
        public List<TopFuncionario> RankingFuncionarios(long idEmpresa)
        {
            try
            {
                return _report.RankingFuncionarios(idEmpresa, DateTime.Now.AddDays(-30).FirstHourOfDay(),
                DateTime.Now.LastHourOfDay());
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
                throw;
            }
        }
    }
}
