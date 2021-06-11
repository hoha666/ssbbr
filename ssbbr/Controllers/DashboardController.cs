using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ssbbr.Models;
using ssbbr.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ssbbr.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        protected ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            DashboardViewModel a = new DashboardViewModel();
            a.gameCount = _context.Games.Count().ToString();
            a.userCount = _context.Users.Count().ToString();
            return View(a);
        }
    }
}
