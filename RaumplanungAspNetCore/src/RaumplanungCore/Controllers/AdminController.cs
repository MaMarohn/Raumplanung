using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaumplanungCore.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RaumplanungCore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            EmailSender email=new EmailSender();
            await email.SendEmailAsync("Jonas","jonasmair@gmx.de", "Test", "Testmessage");
            return View();
        }
    }
}
