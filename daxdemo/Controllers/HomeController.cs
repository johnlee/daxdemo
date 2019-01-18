using daxdemo.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace daxdemo.Controllers
{
    public class HomeController : Controller
    {
        private IDbHandler _dbHandler;

        public HomeController(IDbHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["DBCONNECT"] = await _dbHandler.ConnectTest();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Index");
        }
    }
}
