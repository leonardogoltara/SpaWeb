using AutoMapper;
using GoltaraSolutions.SpaWeb.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;
using GoltaraSoft.SysBeauty.Web.Controllers;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;

namespace GoltaraSolutions.SpaWeb.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ProdutoController : BaseController
    {
        private ProdutoDomainServices sv;
        private IMapper _mapp;
        public ProdutoController(ProdutoDomainServices produtoDomainServices)
        {
            sv = produtoDomainServices;

            MapperConfiguration config = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<ProdutoViewModel, ProdutoModel>();
               cfg.CreateMap<ProdutoModel, ProdutoViewModel>()
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => (!src.Deletado).ToCustomString()));
           });

            _mapp = config.CreateMapper();
        }
        // GET: Produto
        public ActionResult Index(string tipo)
        {
            List<ProdutoModel> listModel = sv.List(UsuarioLogado.IdEmpresa).ToList();
            List<ProdutoViewModel> listView = new List<ProdutoViewModel>();

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

            listModel.ForEach(x => listView.Add(_mapp.Map<ProdutoViewModel>(x)));

            return View(listView);
        }

        // GET: Produto/Details/5
        public ActionResult Details(long id)
        {
            ProdutoModel Produto = sv.Find(id);

            return View(_mapp.Map<ProdutoViewModel>(Produto));
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        public ActionResult Create(ProdutoViewModel objeto)
        {
            try
            {

                sv.Cadastrar(UsuarioLogado.IdEmpresa, objeto.Nome, objeto.Preco);

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

        // GET: Produto/Edit/5
        public ActionResult Edit(long id)
        {
            ProdutoModel Produto = sv.Find(id);

            return View(_mapp.Map<ProdutoViewModel>(Produto));
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, ProdutoViewModel objeto)
        {
            try
            {
                sv.Editar(id, objeto.Nome, objeto.Preco);

                TempData["Nome"] = objeto.Nome;
                TempData["Acao"] = "criado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Edit(id);
            }
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(long id)
        {
            ProdutoModel Produto = sv.Find(id);

            return View(_mapp.Map<ProdutoViewModel>(Produto));
        }

        // POST: Produto/Delete/5
        [HttpPost]
        public ActionResult Delete(long id, ProdutoViewModel objeto)
        {
            try
            {

                sv.Delete(id);

                ProdutoModel p = sv.Find(id);
                TempData["Nome"] = p.Nome;
                TempData["Acao"] = "desativado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        // GET: Produto/Recover/5
        public ActionResult Recover(long id)
        {
            ProdutoModel Produto = sv.Find(id);

            return View(_mapp.Map<ProdutoViewModel>(Produto));
        }

        // POST: Produto/Recover/5
        [HttpPost]
        public ActionResult Recover(long id, ProdutoViewModel objeto)
        {
            try
            {

                sv.Recover(id);

                ProdutoModel p = sv.Find(id);
                TempData["Nome"] = p.Nome;
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
