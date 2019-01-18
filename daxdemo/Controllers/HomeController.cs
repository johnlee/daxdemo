using daxdemo.Data;
using Microsoft.AspNetCore.Mvc;

namespace daxdemo.Controllers
{
    public class HomeController : Controller
    {
        private IDbHandler _dbHandler;

        public HomeController(IDbHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Index");
        }
    }
}
