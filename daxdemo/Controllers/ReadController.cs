using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using daxdemo.Data;
using daxdemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace daxdemo.Controllers
{
    public class ReadController : Controller
    {
        private IDbHandler _dbHandler;

        public ReadController(IDbHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ReadViewModel();
            model.Widgets = await _dbHandler.Read();

            return View(model);
        }
    }
}