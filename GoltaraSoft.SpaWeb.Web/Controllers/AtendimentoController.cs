using AutoMapper;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GoltaraSolutions.Common.Identity.ExtensionMethod;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSoft.SysBeauty.Web.Controllers;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento.ReportViews;
using GoltaraSolutions.Common.Domain.Report;

namespace GoltaraSolutions.SpaWeb.Web.Controllers
{
    [Authorize()]
    public class AtendimentoController : BaseController
    {
        private AtendimentoDomainServices sv;
        private ServicoDomainServices svServico;
        private OrigemDomainServices svOrigem;
        private FuncionarioDomainServices svFuncionario;
        private ClienteDomainServices svCliente;
        private IMapper _mapp;

        public AtendimentoController(AtendimentoDomainServices atendimentoDomainServices,
            FuncionarioDomainServices funcionarioDomainServices,
            ServicoDomainServices servicoDomainServices,
            OrigemDomainServices origemDomainServices,
            ClienteDomainServices clienteDomainServices)
        {
            sv = atendimentoDomainServices;
            svCliente = clienteDomainServices;
            svServico = servicoDomainServices;
            svFuncionario = funcionarioDomainServices;
            svOrigem = origemDomainServices;

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtendimentoViewModel, AtendimentoModel>();
                cfg.CreateMap<AtendimentoModel, AtendimentoViewModel>();

                cfg.CreateMap<AtendimentoViewModel, AtendimentoReportView>();
                cfg.CreateMap<AtendimentoReportView, AtendimentoViewModel>();

                cfg.CreateMap<AtendimentoModel, AtendimentoReportView>();
                cfg.CreateMap<AtendimentoReportView, AtendimentoModel>();

                cfg.CreateMap<List<AtendimentoModel>, List<AtendimentoViewModel>>();

                cfg.CreateMap<FiltrosReportView, ListItem>()
                    .ForMember(dest => dest.Value, opt =>
                        opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Display, opt =>
                        opt.MapFrom(src => src.Nome));

                cfg.CreateMap<ClienteModel, ListItem>()
                    .ForMember(dest => dest.Value, opt =>
                        opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Display, opt =>
                        opt.MapFrom(src => src.Nome));
                cfg.CreateMap<ServicoModel, ListItem>()
                    .ForMember(dest => dest.Value, opt =>
                        opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Display, opt =>
                        opt.MapFrom(src => src.Nome));
                cfg.CreateMap<FuncionarioModel, ListItem>()
                    .ForMember(dest => dest.Value, opt =>
                        opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Display, opt =>
                        opt.MapFrom(src => src.Nome));
                cfg.CreateMap<OrigemModel, ListItem>()
                    .ForMember(dest => dest.Value, opt =>
                        opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Display, opt =>
                        opt.MapFrom(src => src.Nome));
            });

            _mapp = config.CreateMapper();
        }

        public ActionResult Index(AtendimentoReportViewModel report)
        {

            if (report == null)
            {
                report = new AtendimentoReportViewModel();
            }


            if (report.Clientes == null)
            {
                List<FiltrosReportView> clientes = svCliente.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(clientes);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Clientes = new SelectList(itens, "Value", "Display");
            }

            if (report.Servicos == null)
            {
                List<FiltrosReportView> Servicos = svServico.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(Servicos);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Servicos = new SelectList(itens, "Value", "Display");
            }

            if (report.Funcionarios == null)
            {
                List<FiltrosReportView> Funcionarios = svFuncionario.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(Funcionarios);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Funcionarios = new SelectList(itens, "Value", "Display");
            }

            if (report.Origens == null)
            {
                List<FiltrosReportView> origens = svOrigem.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(origens);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Origens = new SelectList(itens, "Value", "Display");
            }


            //if (report.Status == null)
            //{
            //    List<ListItem> itens = new List<ListItem>();
            //    itens.Insert(0, new ListItem("Selecione...", null));
            //    itens.Insert(1, new ListItem("Abertos", null));
            //    itens.Insert(2, new ListItem("Cancelados", null));
            //    itens.Insert(3, new ListItem("Concluidos", null));

            //    report.Status = new SelectList(itens, "Value", "Display");
            //    report.iStatus = 0;
            //}

            switch (report.Status)
            {
                case Status.Aberto:
                    report.Cancelado = false;
                    report.Concluido = false;
                    break;
                case Status.Cancelado:
                    report.Cancelado = true;
                    break;
                case Status.Concluído:
                    report.Concluido = true;
                    break;
                case Status.Todos:
                    report.Cancelado = null;
                    report.Concluido = null;
                    break;
                default:
                    break;
            }

            List<AtendimentoReportView> atendimentos = sv.Relatorio(UsuarioLogado.IdEmpresa, report.DataHoraInicial.FirstHourOfDay(), report.DataHoraFinal.LastHourOfDay(),
                report.IdCliente, report.IdServico, report.IdFuncionario, report.IdOrigem,
                report.Cancelado, report.Concluido);

            //List<AtendimentoViewModel> listView = new List<AtendimentoViewModel>();
            //if (atendimentos.IsNotNull())
            //{
            //    atendimentos.ForEach(o => listView.Add(_mapp.Map<AtendimentoViewModel>(o)));
            //}

            report.Atendimentos = atendimentos;

            return View(report);
        }

        public ActionResult PorCliente(AtendimentoReportAgrupadoViewModel report)
        {

            if (report == null)
            {
                report = new AtendimentoReportAgrupadoViewModel();
            }

            if (report.Clientes == null)
            {
                List<FiltrosReportView> clientes = svCliente.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(clientes);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Clientes = new SelectList(itens, "Value", "Display");
            }

            if (report.Servicos == null)
            {
                List<FiltrosReportView> Servicos = svServico.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(Servicos);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Servicos = new SelectList(itens, "Value", "Display");
            }

            if (report.Funcionarios == null)
            {
                List<FiltrosReportView> Funcionarios = svFuncionario.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(Funcionarios);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Funcionarios = new SelectList(itens, "Value", "Display");
            }

            if (report.Origens == null)
            {
                List<FiltrosReportView> origens = svOrigem.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(origens);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Origens = new SelectList(itens, "Value", "Display");
            }

            switch (report.Status)
            {
                case Status.Aberto:
                    report.Cancelado = false;
                    report.Concluido = false;
                    break;
                case Status.Cancelado:
                    report.Cancelado = true;
                    break;
                case Status.Concluído:
                    report.Concluido = true;
                    break;
                case Status.Todos:
                    report.Cancelado = null;
                    report.Concluido = null;
                    break;
                default:
                    break;
            }

            List<AtendimentoReportAgrupadoView> atendimentos = sv.RelatorioPorCliente(UsuarioLogado.IdEmpresa, report.DataHoraInicial.FirstHourOfDay(), report.DataHoraFinal.LastHourOfDay(),
                report.IdCliente, report.IdServico, report.IdFuncionario, report.IdOrigem,
                report.Cancelado, report.Concluido);

            report.Atendimentos = atendimentos;

            return View(report);
        }

        public ActionResult PorOrigem(AtendimentoReportAgrupadoViewModel report)
        {

            if (report == null)
            {
                report = new AtendimentoReportAgrupadoViewModel();
            }

            if (report.Clientes == null)
            {
                List<FiltrosReportView> clientes = svCliente.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(clientes);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Clientes = new SelectList(itens, "Value", "Display");
            }

            if (report.Servicos == null)
            {
                List<FiltrosReportView> Servicos = svServico.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(Servicos);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Servicos = new SelectList(itens, "Value", "Display");
            }

            if (report.Funcionarios == null)
            {
                List<FiltrosReportView> Funcionarios = svFuncionario.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(Funcionarios);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Funcionarios = new SelectList(itens, "Value", "Display");
            }

            if (report.Origens == null)
            {
                List<FiltrosReportView> origens = svOrigem.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(origens);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Origens = new SelectList(itens, "Value", "Display");
            }

            switch (report.Status)
            {
                case Status.Aberto:
                    report.Cancelado = false;
                    report.Concluido = false;
                    break;
                case Status.Cancelado:
                    report.Cancelado = true;
                    break;
                case Status.Concluído:
                    report.Concluido = true;
                    break;
                case Status.Todos:
                    report.Cancelado = null;
                    report.Concluido = null;
                    break;
                default:
                    break;
            }

            List<AtendimentoReportAgrupadoView> atendimentos = sv.RelatorioPorOrigem(UsuarioLogado.IdEmpresa, report.DataHoraInicial.FirstHourOfDay(), report.DataHoraFinal.LastHourOfDay(),
                report.IdCliente, report.IdServico, report.IdFuncionario, report.IdOrigem,
                report.Cancelado, report.Concluido);

            report.Atendimentos = atendimentos;

            return View(report);
        }

        public ActionResult PorServico(AtendimentoReportAgrupadoViewModel report)
        {

            if (report == null)
            {
                report = new AtendimentoReportAgrupadoViewModel();
            }

            if (report.Clientes == null)
            {
                List<FiltrosReportView> clientes = svCliente.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(clientes);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Clientes = new SelectList(itens, "Value", "Display");
            }

            if (report.Servicos == null)
            {
                List<FiltrosReportView> Servicos = svServico.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(Servicos);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Servicos = new SelectList(itens, "Value", "Display");
            }

            if (report.Funcionarios == null)
            {
                List<FiltrosReportView> Funcionarios = svFuncionario.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(Funcionarios);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Funcionarios = new SelectList(itens, "Value", "Display");
            }

            if (report.Origens == null)
            {
                List<FiltrosReportView> origens = svOrigem.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(origens);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Origens = new SelectList(itens, "Value", "Display");
            }

            switch (report.Status)
            {
                case Status.Aberto:
                    report.Cancelado = false;
                    report.Concluido = false;
                    break;
                case Status.Cancelado:
                    report.Cancelado = true;
                    break;
                case Status.Concluído:
                    report.Concluido = true;
                    break;
                case Status.Todos:
                    report.Cancelado = null;
                    report.Concluido = null;
                    break;
                default:
                    break;
            }

            List<AtendimentoReportAgrupadoView> atendimentos = sv.RelatorioPorServico(UsuarioLogado.IdEmpresa, report.DataHoraInicial.FirstHourOfDay(), report.DataHoraFinal.LastHourOfDay(),
                report.IdCliente, report.IdServico, report.IdFuncionario, report.IdOrigem,
                report.Cancelado, report.Concluido);

            report.Atendimentos = atendimentos;

            return View(report);
        }

        public ActionResult PorFuncionario(AtendimentoReportAgrupadoViewModel report)
        {

            if (report == null)
            {
                report = new AtendimentoReportAgrupadoViewModel();
            }

            if (report.Clientes == null)
            {
                List<FiltrosReportView> clientes = svCliente.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(clientes);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Clientes = new SelectList(itens, "Value", "Display");
            }

            if (report.Servicos == null)
            {
                List<FiltrosReportView> Servicos = svServico.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(Servicos);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Servicos = new SelectList(itens, "Value", "Display");
            }

            if (report.Funcionarios == null)
            {
                List<FiltrosReportView> Funcionarios = svFuncionario.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(Funcionarios);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Funcionarios = new SelectList(itens, "Value", "Display");
            }

            if (report.Origens == null)
            {
                List<FiltrosReportView> origens = svOrigem.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
                List<ListItem> itens = _mapp.Map<List<ListItem>>(origens);
                itens.Insert(0, new ListItem("Selecione...", null));

                report.Origens = new SelectList(itens, "Value", "Display");
            }

            switch (report.Status)
            {
                case Status.Aberto:
                    report.Cancelado = false;
                    report.Concluido = false;
                    break;
                case Status.Cancelado:
                    report.Cancelado = true;
                    break;
                case Status.Concluído:
                    report.Concluido = true;
                    break;
                case Status.Todos:
                    report.Cancelado = null;
                    report.Concluido = null;
                    break;
                default:
                    break;
            }

            List<AtendimentoReportAgrupadoView> atendimentos = sv.RelatorioPorFuncionario(UsuarioLogado.IdEmpresa, report.DataHoraInicial.FirstHourOfDay(), report.DataHoraFinal.LastHourOfDay(),
                report.IdCliente, report.IdServico, report.IdFuncionario, report.IdOrigem,
                report.Cancelado, report.Concluido);

            report.Atendimentos = atendimentos;

            return View(report);
        }

        public ActionResult Agenda()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            DateTime dataIni = DateTime.Now.AddMonths(-3).FirstHourOfDay();
            DateTime dataFin = DateTime.Now.AddMonths(3).LastHourOfDay();

            List<AtendimentoReportView> events = sv.Relatorio(UsuarioLogado.IdEmpresa,
                dataIni, dataFin, null, null, null, null, null, null);

            events.ForEach(e =>
            {
                e.DataHoraEncerramento = e.DataHora.AddMinutes(30);

                e.Status = Status.Aberto.ToString();

                if (e.Cancelado)
                {
                    e.Status = Status.Cancelado.ToString();
                }
                if (e.Concluido)
                {
                    e.Status = Status.Concluído.ToString();
                }

                e.DataHoraEncerramentoString = e.DataHoraEncerramento.ToString("yyyy-MM-dd HH:mm:ss");
                e.DataHoraString = e.DataHora.ToString("yyyy-MM-dd HH:mm:ss");
            });

            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult Create()
        {
            AtendimentoViewModel atendimento = new AtendimentoViewModel();

            FillView(ref atendimento);

            return View(atendimento);
        }

        [HttpPost]
        public ActionResult Create(AtendimentoViewModel o)
        {
            try
            {
                sv.Agendar(UsuarioLogado.IdEmpresa, o.DataHora, o.IdServico, o.IdCliente, o.IdFuncionario, UsuarioLogado.Id);

                TempData["Acao"] = "agendado";

                AtendimentoReportViewModel reporFilter = new AtendimentoReportViewModel();

                if (reporFilter.DataHoraInicial > o.DataHora)
                {
                    reporFilter.DataHoraInicial = o.DataHora.FirstHourOfDay();
                }

                if (reporFilter.DataHoraFinal < o.DataHora)
                {
                    reporFilter.DataHoraFinal = o.DataHora.LastHourOfDay();
                }

                return RedirectToAction("Index", reporFilter);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                FillView(ref o);
                return View(o);
            }
        }

        public ActionResult Edit(long id)
        {
            AtendimentoModel a = sv.Find(id);

            AtendimentoViewModel atendimento = _mapp.Map<AtendimentoViewModel>(a);
            FillView(ref atendimento);

            return View(atendimento);
        }

        [HttpPost]
        public ActionResult Edit(AtendimentoViewModel o)
        {
            try
            {

                sv.Editar(o.Id, o.DataHora, o.IdServico, o.IdCliente, o.IdFuncionario, UsuarioLogado.Id);

                TempData["Acao"] = "editado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                AtendimentoModel a = sv.Find(o.Id);
                o = _mapp.Map<AtendimentoViewModel>(a);

                FillView(ref o);
                return View(o);
            }
        }

        [HttpPost]
        public ActionResult Cancel(AtendimentoViewModel o)
        {
            try
            {
                sv.Cancelar(o.Id);

                TempData["Acao"] = "cancelado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                FillView(ref o);
                return View(o);
            }
        }

        [HttpPost]
        public ActionResult Concluir(AtendimentoViewModel o)
        {
            try
            {
                sv.Concluir(o.Id, o.Valor);

                TempData["Acao"] = "concluido";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                FillView(ref o);
                return View(o);
            }
        }

        public ActionResult Details(long id)
        {
            try
            {
                AtendimentoModel a = sv.Find(id);
                AtendimentoViewModel aVM = _mapp.Map<AtendimentoViewModel>(a);

                aVM.UsuarioAgendou = User.Identity.GetName();

                return View(aVM);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        private void FillView(ref AtendimentoViewModel view)
        {
            List<FiltrosReportView> clientes = svCliente.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
            view.Clientes = new SelectList(clientes, "Id", "Nome");

            List<FiltrosReportView> Servicos = svServico.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
            view.Servicos = new SelectList(Servicos, "Id", "Nome");

            List<FiltrosReportView> Funcionarios = svFuncionario.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
            view.Funcionarios = new SelectList(Funcionarios, "Id", "Nome");

        }

        private class ListItem
        {
            public ListItem()
            {

            }
            public ListItem(string display, string value)
            {
                Display = display;
                Value = value;
            }
            public string Display { get; set; }
            public string Value { get; set; }
        }
    }
}