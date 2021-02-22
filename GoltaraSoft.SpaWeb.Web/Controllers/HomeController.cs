using GoltaraSoft.SysBeauty.Web.Controllers;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSolutions.SpaWeb.Web.Models;
using System.Web.Mvc;

namespace GoltaraSolutions.SpaWeb.Web.Controllers
{
    [Authorize()]
    public class HomeController : BaseController
    {
        private AtendimentoDomainServices sv;
        private ClienteDomainServices svCliente;
        private ServicoDomainServices svServico;
        private OrigemDomainServices svOrigem;
        private FuncionarioDomainServices svFuncionario;
        private EmpresaDomainServices svEmpresa;

        public HomeController(ClienteDomainServices clienteDomainServices,
            ServicoDomainServices servicoDomainServices,
            FuncionarioDomainServices funcionarioDomainServices,
            AtendimentoDomainServices atendimentoDomainServices,
            OrigemDomainServices origemDomainServices,
            EmpresaDomainServices empresaDomainServices)
        {
            svCliente = clienteDomainServices;
            svServico = servicoDomainServices;
            svFuncionario = funcionarioDomainServices;
            sv = atendimentoDomainServices;
            svOrigem = origemDomainServices;
            svEmpresa = empresaDomainServices;
        }

        [Authorize()]
        public ActionResult Index()
        {
            try
            {
                DashboardViewModel dv = new DashboardViewModel()
                {
                    AtendimentosTodos = sv.IndicadorAtendimentosTodos(UsuarioLogado.IdEmpresa),
                    AtendimentosAbertos = sv.IndicadorAtendimentosAbertos(UsuarioLogado.IdEmpresa),
                    AtendimentosCancelados = sv.IndicadorAtendimentosCancelados(UsuarioLogado.IdEmpresa),
                    AtendimentosConcluidos = sv.IndicadorAtendimentosConcluidos(UsuarioLogado.IdEmpresa),
                    Top10Clientes = sv.Top10Clientes(UsuarioLogado.IdEmpresa),
                    Top10Funcionarios = sv.RankingFuncionarios(UsuarioLogado.IdEmpresa),

                    ClientesAniversariantesMes = svCliente.AniversariantesMes(UsuarioLogado.IdEmpresa)
                };

                return View(dv);
            }
            catch (System.Exception ex)
            {
                Logger.Log.Error(ex);
                RedirectToAction("Login", "Account", null);
            }

            return View();
        }
        [Authorize()]
        public ActionResult AgendaDemo()
        {
            return View();
        }
        [Authorize()]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize()]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize()]
        public ActionResult PopularBancoTeste()
        {

            svEmpresa.PopularBancoTeste(UsuarioLogado.IdEmpresa);

            ViewBag.Message = "Banco de teste populado com sucesso.";

            return View();
        }
    }
}