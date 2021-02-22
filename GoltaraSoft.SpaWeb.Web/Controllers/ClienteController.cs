using AutoMapper;
using GoltaraSolutions.SpaWeb.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSoft.SysBeauty.Web.Controllers;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.AgendaContext.Atendimento.ReportViews;
using GoltaraSolutions.Common.Domain.Report;

namespace GoltaraSolutions.SpaWeb.Web.Controllers
{
    [Authorize()]
    public class ClienteController : BaseController
    {
        private ClienteDomainServices sv;
        private OrigemDomainServices svOrigem;
        private AtendimentoDomainServices svAtendimento;
        private IMapper _mapp;
        public ClienteController(ClienteDomainServices clienteDomainServices,
            OrigemDomainServices origemDomainServices,
            AtendimentoDomainServices atendimentoDomainServices)
        {
            sv = clienteDomainServices;
            svOrigem = origemDomainServices;
            svAtendimento = atendimentoDomainServices;

            MapperConfiguration config = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<ClienteViewModel, ClienteModel>();
               cfg.CreateMap<AtendimentoModel, AtendimentoViewModel>();
               cfg.CreateMap<ClienteModel, ClienteViewModel>()
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => (!src.Deletado).ToCustomString()))
                .ForMember(dest => dest.Origens, opt => opt.Ignore())
                .ForMember(dest => dest.OrigemSelecionada, opt => opt.Ignore()); ;
           });

            _mapp = config.CreateMapper();
        }
        // GET: Cliente
        public ActionResult Index(string tipo)
        {
            List<ClienteModel> listModel = sv.List(UsuarioLogado.IdEmpresa).ToList();
            List<ClienteViewModel> listView = new List<ClienteViewModel>();

            switch (tipo)
            {
                case "Ativos":
                    listModel = listModel.Where(i => i.Deletado == false).ToList();
                    ViewBag.Filtro = "Ativos";

                    break;
                case "Inativos":
                    listModel = listModel.Where(i => i.Deletado == true).ToList();
                    ViewBag.Filtro = "Inativos";

                    break;
                case "Todos":
                    listModel = listModel.ToList();
                    ViewBag.Filtro = "Todos";

                    break;
                default:
                    listModel = listModel.Where(i => i.Deletado == false).ToList();
                    ViewBag.Filtro = "Ativos";

                    break;
            }

            listModel.ForEach(x =>
            {
                ClienteViewModel cliView = _mapp.Map<ClienteViewModel>(x);
                OrigemModel Origem = svOrigem.Find(x.IdOrigem);
                if (Origem.IsNotNull())
                    cliView.Origem = Origem;
                listView.Add(cliView);
            }
            );

            return View(listView);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(long id)
        {
            ClienteModel Cliente = sv.Find(id);
            ClienteViewModel ClienteView = _mapp.Map<ClienteViewModel>(Cliente);

            List<FiltrosReportView> Origens = svOrigem.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
            ClienteView.Origens = new SelectList(Origens, "Id", "Nome");
            ClienteView.OrigemSelecionada = Cliente.IdOrigem;

            OrigemModel Origem = svOrigem.Find(Cliente.IdOrigem);
            if (Origem.IsNotNull())
                ClienteView.Origem = Origem;

            List<AtendimentoReportView> atendimentos = svAtendimento.Relatorio(UsuarioLogado.IdEmpresa, DateTime.Now.AddMonths(-6).FirstDayOfMonth(),
                DateTime.Now.LastHourOfDay(),
                id,
                null,
                null,
                null,
                null,
                null);

            //List<AtendimentoViewModel> listView = new List<AtendimentoViewModel>();
            //if (atendimentos.IsNotNull())
            //{
            //    atendimentos.ForEach(o => listView.Add(_mapp.Map<AtendimentoViewModel>(o)));
            //}

            ClienteView.Historico = atendimentos;

            return View(ClienteView);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            ClienteViewModel Cliente = new ClienteViewModel();

            List<FiltrosReportView> Origens = svOrigem.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
            Cliente.Origens = new SelectList(Origens, "Id", "Nome");

            return View(Cliente);
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(ClienteViewModel objeto)
        {
            try
            {
                
                sv.Cadastrar(UsuarioLogado.IdEmpresa, objeto.Nome, objeto.DataNascimento, objeto.Telefone, objeto.Celular, objeto.Email, objeto.Sexo, objeto.OrigemSelecionada);

                TempData["Nome"] = objeto.Nome;
                TempData["Acao"] = "criado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return Create();
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(long id)
        {
            ClienteModel Cliente = sv.Find(id);
            ClienteViewModel ClienteView = _mapp.Map<ClienteViewModel>(Cliente);

            List<FiltrosReportView> Origens = svOrigem.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
            ClienteView.Origens = new SelectList(Origens, "Id", "Nome");
            ClienteView.OrigemSelecionada = Cliente.IdOrigem;

            OrigemModel Origem = svOrigem.Find(Cliente.IdOrigem);
            if (Origem.IsNotNull())
                ClienteView.Origem = Origem;

            return View(ClienteView);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, ClienteViewModel objeto)
        {
            try
            {
                
                sv.Editar(objeto.Id, objeto.Nome, objeto.DataNascimento, objeto.Telefone, objeto.Celular, objeto.Email, objeto.Sexo, objeto.OrigemSelecionada);

                TempData["Nome"] = objeto.Nome;
                TempData["Acao"] = "editado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);


                ClienteModel Cliente = sv.Find(id);
                ClienteViewModel ClienteView = _mapp.Map<ClienteViewModel>(Cliente);

                List<FiltrosReportView> Origens = svOrigem.ListarFiltro(UsuarioLogado.IdEmpresa).ToList();
                ClienteView.Origens = new SelectList(Origens, "Id", "Nome");
                ClienteView.OrigemSelecionada = Cliente.IdOrigem;

                OrigemModel Origem = svOrigem.Find(Cliente.IdOrigem);
                if (Origem.IsNotNull())
                    ClienteView.Origem = Origem;

                return View(ClienteView);
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(long id)
        {
            ClienteModel Cliente = sv.Find(id);

            return View(_mapp.Map<ClienteViewModel>(Cliente));
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(long id, ClienteViewModel objeto)
        {
            try
            {
                
                sv.Delete(id);

                ClienteModel c = sv.Find(id);

                TempData["Nome"] = c.Nome;
                TempData["Acao"] = "desativado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        // GET: Cliente/Recover/5
        public ActionResult Recover(long id)
        {
            ClienteModel Cliente = sv.Find(id);

            return View(_mapp.Map<ClienteViewModel>(Cliente));
        }

        // POST: Cliente/Recover/5
        [HttpPost]
        public ActionResult Recover(long id, ClienteViewModel objeto)
        {
            try
            {
                
                sv.Recover(id);
                ClienteModel c = sv.Find(id);

                TempData["Nome"] = c.Nome;
                TempData["Acao"] = "ativado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }
    }
}
