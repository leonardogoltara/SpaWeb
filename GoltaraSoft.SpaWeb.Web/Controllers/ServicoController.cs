using AutoMapper;
using GoltaraSolutions.SpaWeb.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using GoltaraSoft.SysBeauty.Web.Controllers;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;

namespace GoltaraSolutions.SpaWeb.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ServicoController : BaseController
    {
        private ServicoDomainServices sv;
        private IMapper _mapp;
        public ServicoController(ServicoDomainServices servicoDomainServices)
        {
            sv = servicoDomainServices;

            MapperConfiguration config = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<ServicoViewModel, ServicoModel>();
               cfg.CreateMap<ServicoModel, ServicoViewModel>()
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => (!src.Deletado).ToCustomString()))
                .ForMember(dest => dest.PrecoFixoString, opt => opt.MapFrom(src => (src.PrecoFixo).ToCustomString())); ;
           });

            _mapp = config.CreateMapper();
        }
        // GET: Servico
        public ActionResult Index(string tipo)
        {
            List<ServicoModel> listModel = sv.List(UsuarioLogado.IdEmpresa).ToList();
            List<ServicoViewModel> listView = new List<ServicoViewModel>();

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

            listModel.ForEach(x => listView.Add(_mapp.Map<ServicoViewModel>(x)));

            return View(listView);
        }

        // GET: Servico/Details/5
        public ActionResult Details(long id)
        {
            ServicoModel Servico = sv.Find(id);

            return View(_mapp.Map<ServicoViewModel>(Servico));
        }

        // GET: Servico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servico/Create
        [HttpPost]
        public ActionResult Create(ServicoViewModel objeto)
        {
            try
            {
                
                sv.Cadastrar(UsuarioLogado.IdEmpresa, objeto.Nome, objeto.Preco, objeto.PrecoFixo);

                TempData["Nome"] = objeto.Nome;
                TempData["Acao"] = "criado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        // GET: Servico/Edit/5
        public ActionResult Edit(long id)
        {
            ServicoModel Servico = sv.Find(id);

            return View(_mapp.Map<ServicoViewModel>(Servico));
        }

        // POST: Servico/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, ServicoViewModel objeto)
        {
            try
            {
                
                sv.Editar(id, objeto.Nome, objeto.Preco, objeto.PrecoFixo);

                TempData["Nome"] = objeto.Nome;
                TempData["Acao"] = "editado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Edit(id);
            }
        }

        // GET: Servico/Delete/5
        public ActionResult Delete(long id)
        {
            ServicoModel Servico = sv.Find(id);

            return View(_mapp.Map<ServicoViewModel>(Servico));
        }

        // POST: Servico/Delete/5
        [HttpPost]
        public ActionResult Delete(long id, ServicoViewModel objeto)
        {
            try
            {
                
                sv.Delete(id);

                ServicoModel s = sv.Find(id);
                TempData["Nome"] = s.Nome;
                TempData["Acao"] = "criado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        // GET: Servico/Recover/5
        public ActionResult Recover(long id)
        {
            ServicoModel Servico = sv.Find(id);

            return View(_mapp.Map<ServicoViewModel>(Servico));
        }

        // POST: Servico/Recover/5
        [HttpPost]
        public ActionResult Recover(long id, ServicoViewModel objeto)
        {
            try
            {
                
                sv.Recover(id);

                ServicoModel s = sv.Find(id);
                TempData["Nome"] = s.Nome;
                TempData["Acao"] = "criado";

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
