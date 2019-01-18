using System.Diagnostics;
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var model = new ReadViewModel();
            model.Widgets = await _dbHandler.Read();

            ViewData["TIME"] = stopwatch.Elapsed;
            stopwatch.Stop();

            return View(model);
        }
    }
}