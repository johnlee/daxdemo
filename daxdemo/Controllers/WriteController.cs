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

        [HttpPost]
        public async Task<IActionResult> Create(WriteViewModel model)
        {
            if (ModelState.IsValid)
            {
                Widget widget = new Widget()
                {
                    pk = model.Name,
                    sk = model.Date.ToShortDateString(),
                    data = model.Description
                };

                await _dbHandler.Write(widget);

                ViewData["RESULT"] = true;
            }
            else
            {
                ViewData["MESSAGE"] = "Invalid input please try again";
            }
            return View("Index");
        }
    }
}