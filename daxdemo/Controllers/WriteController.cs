using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using daxdemo.Data;
using daxdemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace daxdemo.Controllers
{
    public class WriteController : Controller
    {
        private IDbHandler _dbHandler;

        public WriteController(IDbHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        public IActionResult Index()
        {
            return View(new WriteViewModel());
        }
    }
}