using AutoMapper;
using GoltaraSolutions.SpaWeb.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSoft.SysBeauty.Web.Controllers;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.Common.Domain.Report;

namespace GoltaraSolutions.SpaWeb.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class FuncionarioController : BaseController
    {
        private FuncionarioDomainServices sv;
        private ServicoDomainServices svServico;
        private IMapper _mapp;
        public FuncionarioController(FuncionarioDomainServices funcionarioDomainServices,
            ServicoDomainServices servicoDomainServices)
        {
            sv = funcionarioDomainServices;
            svServico = servicoDomainServices;

            MapperConfiguration config = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<FuncionarioViewModel, FuncionarioModel>();

               cfg.CreateMap<FuncionarioModel, FuncionarioViewModel>()
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => (!src.Deletado).ToCustomString()))
                .ForMember(dest => dest.ServicosSelecionados, opt => opt.Ignore())
                .ForMember(dest => dest.ServicosPossiveis, opt => opt.Ignore())
                .ForMember(dest => dest.ServicosPrestados, opt => opt.Ignore()); ;
           });

            _mapp = config.CreateMapper();
        }
        // GET: Funcionario
        public ActionResult Index(string tipo)
        {
            List<FuncionarioModel> listModel = sv.List(UsuarioLogado.IdEmpresa).ToList();
            List<FuncionarioViewModel> listView = new List<FuncionarioViewModel>();

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
                FuncionarioViewModel funcView = _mapp.Map<FuncionarioViewModel>(x);
                listView.Add(funcView);
            }
            );

            return View(listView);
        }

        // GET: Funcionario/Details/5
        public ActionResult Details(long id)
        {
            FuncionarioModel Funcionario = sv.Find(id);
            FuncionarioViewModel funcionarioView = _mapp.Map<FuncionarioViewModel>(Funcionario);

            List<FiltrosReportView> servicos = svServico.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();

            funcionarioView.ServicosPossiveis = new MultiSelectList(servicos, "Id", "Nome");
            funcionarioView.ServicosSelecionados = Funcionario.Servicos?.Select(s => s.Id).ToArray();

            if (Funcionario.Servicos.IsNotNull())
                Funcionario.Servicos.ToList().ForEach(y =>
                {
                    funcionarioView.ServicosPrestados.Add(y);
                }
                );

            return View(funcionarioView);
        }

        // GET: Funcionario/Create
        public ActionResult Create()
        {
            FuncionarioViewModel Funcionario = new FuncionarioViewModel();

            List<FiltrosReportView> servicos = svServico.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
            
            Funcionario.ServicosPossiveis = new MultiSelectList(servicos, "Id", "Nome");
            Funcionario.ServicosSelecionados = new long[] { };

            return View(Funcionario);
        }

        // POST: Funcionario/Create
        [HttpPost]
        public ActionResult Create(FuncionarioViewModel objeto)
        {
            try
            {


                sv.Cadastrar(UsuarioLogado.IdEmpresa,
                    objeto.Nome,
                    objeto.DataNascimento,
                    objeto.Telefone,
                    objeto.Celular,
                    objeto.Email,
                    objeto.Sexo,
                    objeto.ServicosSelecionados);

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

        // GET: Funcionario/Edit/5
        public ActionResult Edit(long id)
        {
            FuncionarioModel Funcionario = sv.Find(id);
            FuncionarioViewModel funcionarioView = _mapp.Map<FuncionarioViewModel>(Funcionario);

            List<FiltrosReportView> servicos = svServico.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();

            funcionarioView.ServicosPossiveis = new MultiSelectList(servicos, "Id", "Nome");
            funcionarioView.ServicosSelecionados = Funcionario.Servicos?.Select(s => s.Id).ToArray();

            return View(funcionarioView);
        }

        // POST: Funcionario/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, FuncionarioEditViewModel objeto)
        {
            try
            {


                sv.Editar(objeto.Id, objeto.Nome, objeto.DataNascimento, objeto.Telefone, objeto.Celular, objeto.Email, objeto.Sexo, objeto.ServicosSelecionados);

                TempData["Nome"] = objeto.Nome;
                TempData["Acao"] = "editado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);


                FuncionarioModel Funcionario = sv.Find(id);
                FuncionarioViewModel funcionarioView = _mapp.Map<FuncionarioViewModel>(Funcionario);

                List<FiltrosReportView> servicos = svServico.ListarFiltros(UsuarioLogado.IdEmpresa).ToList();
   
                funcionarioView.ServicosPossiveis = new MultiSelectList(servicos, "Id", "Nome");
                funcionarioView.ServicosSelecionados = Funcionario.Servicos?.Select(s => s.Id).ToArray();

                return View(funcionarioView);
            }
        }

        // GET: Funcionario/Delete/5
        public ActionResult Delete(long id)
        {
            FuncionarioModel Funcionario = sv.Find(id);

            return View(_mapp.Map<FuncionarioViewModel>(Funcionario));
        }

        // POST: Funcionario/Delete/5
        [HttpPost]
        public ActionResult Delete(long id, FuncionarioViewModel objeto)
        {
            try
            {

                sv.Delete(id);

                FuncionarioModel f = sv.Find(id);
                TempData["Nome"] = f.Nome;
                TempData["Acao"] = "desativado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        // GET: Funcionario/Recover/5
        public ActionResult Recover(long id)
        {
            FuncionarioModel Funcionario = sv.Find(id);

            return View(_mapp.Map<FuncionarioViewModel>(Funcionario));
        }

        // POST: Funcionario/Recover/5
        [HttpPost]
        public ActionResult Recover(long id, FuncionarioViewModel objeto)
        {
            try
            {

                sv.Recover(id);

                FuncionarioModel f = sv.Find(id);
                TempData["Nome"] = f.Nome;
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
