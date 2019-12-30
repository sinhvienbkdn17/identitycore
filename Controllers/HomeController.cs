using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreIdentityExample.Models;
using Microsoft.AspNetCore.Authorization;

namespace DotNetCoreIdentityExample.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index() => View(GetData(nameof(Index)));

        [Authorize(Roles = "Users")]
        public IActionResult OtherAction() => View("Index", GetData(nameof(OtherAction)));

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Dictionary<string, object> GetData(string actionName) =>
                                                                    new Dictionary<string, object>
                                                                    {
                                                                        ["Action"] = actionName,
                                                                        ["User"] = HttpContext.User.Identity.Name,
                                                                        ["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
                                                                        ["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
                                                                        ["In Users Role"] = HttpContext.User.IsInRole("Users")
                                                                    };
    }
}
