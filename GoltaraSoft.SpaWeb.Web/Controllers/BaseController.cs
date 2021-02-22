using GoltaraSolutions.Common.Identity.ExtensionMethod;
using GoltaraSolutions.Common.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoltaraSoft.SysBeauty.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Usuario UsuarioLogado
        {
            get
            {
                return User.Identity.GetUsuario();
            }
        }
    }
}