using GoltaraSolutions.Common.Identity.Configuration;
using GoltaraSolutions.Common.Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;

namespace GoltaraSolutions.SpaWeb.Web.Services
{
    public class AccountServices
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;
        public AccountServices(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public string Create(CadastroViewModel usuario)
        {
            Usuario user = new Usuario();
            user.UserName = usuario.Email;
            user.Nome = usuario.Nome;
            user.Email = usuario.Email;
            user.Deletado = usuario.Deletado;

            Usuario usr;
            IdentityResult result = _userManager.Create(user, usuario.Senha);
            if (result.Succeeded)
            {
                usr = _userManager.FindByEmail(user.Email);
                _userManager.AddToRoles(usr.Id, new[] { usuario.Grupo.ToString() });

                return usr.Id;
            }
            else
            {
                if (result.Errors != null)
                {
                    string erros = "Erro ao criar usuário. ";
                    foreach (string error in result.Errors)
                    {
                        erros += error + "; ";
                    }
                    throw new System.Exception(erros);
                }
            }

            return null;
        }

        public void Edit(EditarViewModel usuario, Guid idUsuario)
        {
            Usuario user = new Usuario();
            user.Id = idUsuario.ToString();
            user.UserName = usuario.Nome;
            user.Nome = usuario.Nome;
            user.Email = usuario.Email;
            user.Deletado = usuario.Deletado;
            var usr = _userManager.FindById(user.Id);

            if (usr == null)
            {
                // Não encontrou o usuário.
            }

            usr.Nome = user.Nome;
            usr.Email = user.Email;
            usr.UserName = user.Email;

            var result = _userManager.Update(usr);
            if (result.Succeeded)
            {
                // Remove roles
                _userManager.RemoveFromRole(usr.Id, GrupoDeAcesso.Administrador.ToString());
                _userManager.RemoveFromRole(usr.Id, GrupoDeAcesso.Usuario.ToString());

                // Adiciona role nova
                _userManager.AddToRole(usr.Id, usuario.Grupo.ToString());
            }
            else
            {
                if (result.Errors != null)
                {
                    string erros = "Erro ao editar usuário. ";
                    foreach (string error in result.Errors)
                    {
                        erros += error + "; ";
                    }
                    throw new System.Exception(erros);
                }
            }
        }

        public void Delete(string userID)
        {
            var usr = _userManager.FindById(userID);

            if (usr == null)
            {
                // Não encontrou o usuário.
            }
            usr.Deletado = true;

            var result = _userManager.Update(usr);
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    string erros = "Erro ao desabilitar usuário. ";
                    foreach (string error in result.Errors)
                    {
                        erros += error + "; ";
                    }
                    throw new System.Exception(erros);
                }

            }

        }

        public void Recover(string userID)
        {
            var usr = _userManager.FindById(userID);

            if (usr == null)
            {
                // Não encontrou o usuário.
            }
            usr.Deletado = false;

            var result = _userManager.Update(usr);
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    string erros = "Erro ao desabilitar usuário. ";
                    foreach (string error in result.Errors)
                    {
                        erros += error + "; ";
                    }
                    throw new Exception(erros);
                }

            }

        }

        public bool HasAnyUser()
        {
            return _userManager.Users.Any();
        }

        public void RegistraRegras()
        {
            if (_roleManager.FindByName("Administrador") == null)
            {
                _roleManager.Create(new IdentityRole("Administrador"));
                _roleManager.Create(new IdentityRole("Usuario"));
            }
        }

    }
}