using AutoMapper;
using GoltaraSolutions.SpaWeb.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSoft.SysBeauty.Web.Controllers;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;

namespace GoltaraSolutions.SpaWeb.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class OrigemController : BaseController
    {
        private OrigemDomainServices sv;
        private IMapper _mapp;
        public OrigemController(OrigemDomainServices OrigemDomainServices)
        {
            sv = OrigemDomainServices;

            MapperConfiguration config = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<OrigemViewModel, OrigemModel>();
               cfg.CreateMap<OrigemModel, OrigemViewModel>()
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => (!src.Deletado).ToCustomString())); ;
           });

            _mapp = config.CreateMapper();
        }
        // GET: Origem
        public ActionResult Index(string tipo)
        {
            List<OrigemModel> listModel = sv.List(UsuarioLogado.IdEmpresa).ToList();
            List<OrigemViewModel> listView = new List<OrigemViewModel>();

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

            listModel.ForEach(x => listView.Add(_mapp.Map<OrigemViewModel>(x)));

            return View(listView);
        }

        // GET: Origem/Details/5
        public ActionResult Details(long id)
        {
            OrigemModel origem = sv.Find(id);

            return View(_mapp.Map<OrigemViewModel>(origem));
        }

        // GET: Origem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Origem/Create
        [HttpPost]
        public ActionResult Create(OrigemViewModel objeto)
        {
            try
            {

                
                sv.Cadastrar(UsuarioLogado.IdEmpresa, objeto.Nome);

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

        // GET: Origem/Edit/5
        public ActionResult Edit(long id)
        {
            OrigemModel origem = sv.Find(id);

            return View(_mapp.Map<OrigemViewModel>(origem));
        }

        // POST: Origem/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, OrigemViewModel objeto)
        {
            try
            {
                
                sv.Editar(id, objeto.Nome);

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

        // GET: Origem/Delete/5
        public ActionResult Delete(long id)
        {
            OrigemModel origem = sv.Find(id);

            return View(_mapp.Map<OrigemViewModel>(origem));
        }

        // POST: Origem/Delete/5
        [HttpPost]
        public ActionResult Delete(long id, OrigemViewModel objeto)
        {
            try
            {
                
                sv.Delete(id);

                OrigemModel o = sv.Find(id);
                TempData["Nome"] = o.Nome;
                TempData["Acao"] = "desativado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        // GET: Origem/Recover/5
        public ActionResult Recover(long id)
        {
            OrigemModel origem = sv.Find(id);

            return View(_mapp.Map<OrigemViewModel>(origem));
        }

        // POST: Origem/Recover/5
        [HttpPost]
        public ActionResult Recover(long id, OrigemViewModel objeto)
        {
            try
            {
                
                sv.Recover(id);

                OrigemModel o = sv.Find(id);
                TempData["Nome"] = o.Nome;
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
