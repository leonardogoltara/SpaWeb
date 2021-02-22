using GoltaraSoft.SysBeauty.Web.Controllers;
using GoltaraSolutions.Common.Extensions;
using GoltaraSolutions.Common.Identity.Configuration;
using GoltaraSolutions.Common.Identity.Models;
using GoltaraSolutions.Common.Infra.Log;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GoltaraSolutions.SpaWeb.Web.Controllers
{
    public class AccountController : BaseController
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;
        private EmpresaDomainServices _svEmpresa;

        public AccountController(IEmpresaRepository empresaRepository,
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            ApplicationSignInManager signInManager,
            EmpresaDomainServices empresaDomainServices)
        {
            _svEmpresa = empresaDomainServices;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            // Redireciona para o Registro de Funcionário se não existir 
            if (_userManager.Users.Count() <= 0)
            {
                return RedirectToAction("Registrar", "Account");
            }

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email.Trim(),
                model.Senha.Trim(),
                model.LembrarMe,
                shouldLockout: false);

            switch (result)
            {
                case Microsoft.AspNet.Identity.Owin.SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case Microsoft.AspNet.Identity.Owin.SignInStatus.LockedOut:
                    return View("Lockout");
                case Microsoft.AspNet.Identity.Owin.SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Email ou Senha incorretos");
                    return View();
            }
        }

        [AllowAnonymous]
        public ActionResult Registrar()
        {
            //if (_userManager.Users.Any())
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrar(GoltaraSoft.SysBeauty.Web.Models.RegistrarViewModel model)
        {
            if (ModelState.IsValid)
            {
                EmpresaModel empresa = null;
                try
                {
                    _svEmpresa.Cadastrar(model.EmpresaCNPJ, model.EmpresaNome, model.Nome, model.Celular, model.Email);
                    empresa = _svEmpresa.FindCNPJ(model.EmpresaCNPJ);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                    return View(model);
                }
                if (empresa != null)
                {
                    var user = new Usuario
                    {
                        Nome = model.Nome,
                        UserName = model.Email,
                        Email = model.Email,
                        IdEmpresa = empresa.Id
                    };

                    var result = await _userManager.CreateAsync(user, model.Senha);
                    if (result.Succeeded)
                    {
                        if (_roleManager.FindByName("Administrador") == null)
                        {
                            _roleManager.Create(new IdentityRole("Administrador"));
                            _roleManager.Create(new IdentityRole("Usuario"));
                        }

                        Usuario usr = _userManager.FindByEmail(model.Email);

                        await _userManager.AddToRolesAsync(usr.Id, new[] { "Administrador" });

                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }

                ModelState.AddModelError(string.Empty, "Houve um erro ao tentar cadastrar a empresa.");
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult EsqueciMinhaSenha()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EsqueciMinhaSenha(EsqueciMinhaSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // Não revelar se o usuario nao existe ou nao esta confirmado
                    return View("EsqueciMinhaSenhaConfirmacao");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("RecoverSenha", "Account", new { userId = user.Id, codigo = code }, protocol: Request.Url.Scheme);
                string htmlEmailBody = @"
                    <html>
                    <head>
                    </head>
                    <body style='    
	                    font-family: 'Open Sans','Helvetica Neue',Helvetica,Arial,sans-serif;
                        font-weight: 200;
                        line-height: 1.42857143;
                        color: #777777;
                        background-color: #eee;
                        padding: 100px;'>
	                    <div style='
		                    margin-bottom: 18px;
	                        background-color: #ffffff;
	                        border: 1px solid rgba(119, 119, 119, 0.46);
	                        border-radius: 4px;
	                        -webkit-box-shadow: 0 1px 1px rgba(0,0,0,0.05);
	                        box-shadow: 0 1px 1px rgba(0,0,0,0.05);
	                        height: 150px;'>
		                    <div style='
		                        padding: 10px 15px;
		                        border-bottom: 1px solid rgba(119, 119, 119, 0.46);
		                        border-top-right-radius: 3px;
		                        border-top-left-radius: 3px;
		                        box-sizing: border-box;
		                        background-color: #d9230f;
		                        color: #ffffff;'>
			                    <h3 style='
				                    font-weight: 300;
				                    line-height: 1.1;
				                    margin-top: 0;
			                        margin-bottom: 0;
			                        font-size: 15px;
			                        color: inherit;'>
				                    SpaWeb - Esqueci minha senha
			                    </h3>
		                    </div>
		                    <div style='padding: 15px;'>
			                    <h2>Olá, " + user.Nome + @".</h2>
			                    <p>Para Recover sua senha, clique <a href='" + callbackUrl + @"'>aqui</a> e escolha uma nova senha.</p>
		                    </div>
	                    </div>
                    </body>
                    </html>";

                await _userManager.SendEmailAsync(user.Id, "SpaWeb - Esqueci minha senha", htmlEmailBody);
                return View("EsqueciMinhaSenhaConfirmacao");
            }

            // No caso de falha, reexibir a view. 
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult EsqueciMinhaSenhaConfirmacao()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult RecoverSenha(string userId, string codigo)
        {
            RecuperarSenhaViewModel model = new RecuperarSenhaViewModel();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                model.Email = _userManager.FindById(userId).Email;
                model.Codigo = codigo;
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecoverSenha(RecuperarSenhaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("Login", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Codigo, model.Senha);
            if (result.Succeeded)
            {
                TempData["Mensagem"] = "Legal! Senha recuperada com sucesso.";
                return RedirectToAction("Login", "Account");
            }
            AddErrors(result);
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Create(CadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario
                {
                    Nome = model.Nome,
                    UserName = model.Email,
                    Email = model.Email,
                    IdEmpresa = UsuarioLogado.IdEmpresa
                    //DataNascimento = model.DataNascimento,
                    //CPF = model.CPF,
                    //Telefone = model.Telefone,
                    //Celular = model.Celular,
                };

                var result = await _userManager.CreateAsync(user, model.Senha);
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    Usuario usr = _userManager.FindByEmail(model.Email);

                    await _userManager.AddToRolesAsync(usr.Id, new[] { model.Grupo.ToString() });

                    TempData["Nome"] = usr.Nome;
                    TempData["Acao"] = "criado";
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            return View(model);
        }

        [Authorize(Roles = "Administrador, Qualidade")]
        public ActionResult Index(string tipo)
        {
            List<Usuario> listModel = _userManager.Users
                .Where(u => u.IdEmpresa == UsuarioLogado.IdEmpresa)
                .ToList();

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

            List<DetalhesViewModel> lista = new List<DetalhesViewModel>();
            if (listModel.IsNotNull())
            {
                foreach (Usuario usr in listModel)
                {
                    DetalhesViewModel model = new DetalhesViewModel()
                    {
                        Id = usr.Id,
                        Nome = usr.Nome,
                        Email = usr.Email,
                        Deletado = usr.Deletado,
                    };

                    var role = _roleManager.FindByIdAsync(usr.Roles.FirstOrDefault().RoleId).Result;
                    switch (role.Name)
                    {
                        case "Administrador":
                            model.Grupo = GrupoDeAcesso.Administrador;
                            break;

                        case "Usuario":
                            model.Grupo = GrupoDeAcesso.Usuario;
                            break;
                    }

                    lista.Add(model);
                }
            }

            return View(lista);
        }

        public async Task<ActionResult> Details(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var usr = await _userManager.FindByIdAsync(id);
                if (usr != null)
                {
                    DetalhesViewModel model = new DetalhesViewModel()
                    {
                        Id = usr.Id,
                        Nome = usr.Nome,
                        Email = usr.Email,
                        //Telefone = usr.Telefone,
                        //Celular = usr.Celular,
                        //DataNascimento = usr.DataNascimento,
                        //CPF = usr.CPF,
                        Deletado = usr.Deletado,
                    };

                    var role = await _roleManager.FindByIdAsync(usr.Roles.FirstOrDefault().RoleId);
                    switch (role.Name)
                    {
                        case "Administrador":
                            model.Grupo = GrupoDeAcesso.Administrador;
                            break;

                        case "Usuario":
                            model.Grupo = GrupoDeAcesso.Usuario;
                            break;
                    }

                    return View(model);
                }

                // Se não encontrou usuário pelo ID volta para a listagem
                return RedirectToAction("Index");
            }

            // Se id está vazio volta para a listagem
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Edit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var usr = await _userManager.FindByIdAsync(id);
                if (usr == null)
                {
                    // Se não encontrou usuário pelo ID volta para a listagem
                    return RedirectToAction("Index");
                }

                EditarViewModel model = new EditarViewModel()
                {
                    Id = usr.Id,
                    Nome = usr.Nome,
                    Email = usr.Email,
                    //Telefone = usr.Telefone,
                    //Celular = usr.Celular,
                    //DataNascimento = usr.DataNascimento,
                    //CPF = usr.CPF
                };

                var role = await _roleManager.FindByIdAsync(usr.Roles.FirstOrDefault().RoleId);
                switch (role.Name)
                {
                    case "Administrador":
                        model.Grupo = GrupoDeAcesso.Administrador;
                        break;

                    case "Usuario":
                        model.Grupo = GrupoDeAcesso.Usuario;
                        break;
                }
                return View(model);
            }

            // Se id está vazio volta para a listagem
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Edit(EditarViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usr = await _userManager.FindByIdAsync(model.Id);

                    if (usr == null)
                    {
                        return RedirectToAction("Index");
                    }

                    usr.Nome = model.Nome;
                    usr.Email = model.Email;
                    usr.IdEmpresa = UsuarioLogado.IdEmpresa;
                    //usr.Celular = model.Celular;
                    //usr.CPF = model.CPF;
                    //usr.DataNascimento = model.DataNascimento;
                    usr.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(usr);
                    if (result.Succeeded)
                    {
                        // Remove roles
                        _userManager.RemoveFromRole(usr.Id, GrupoDeAcesso.Administrador.ToString());
                        _userManager.RemoveFromRole(usr.Id, GrupoDeAcesso.Usuario.ToString());

                        // Adiciona role nova
                        _userManager.AddToRole(usr.Id, model.Grupo.ToString());
                    }

                    TempData["Nome"] = usr.Nome;
                    TempData["Acao"] = "editado";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex);
                    throw;
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var usr = await _userManager.FindByIdAsync(id);
                usr.Deletado = true;

                await _userManager.UpdateAsync(usr);

                TempData["Nome"] = usr.Nome;
                TempData["Acao"] = "desativado";
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Recover(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var usr = await _userManager.FindByIdAsync(id);
                usr.Deletado = false;

                await _userManager.UpdateAsync(usr);

                TempData["Nome"] = usr.Nome;
                TempData["Acao"] = "ativado";
            }

            return RedirectToAction("Index");
        }

        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AlterarSenha(AlterarSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userManager.RemovePasswordAsync(User.Identity.GetUserId());
                await _userManager.AddPasswordAsync(User.Identity.GetUserId(), model.Senha);
                TempData["Acao"] = "senha";

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}