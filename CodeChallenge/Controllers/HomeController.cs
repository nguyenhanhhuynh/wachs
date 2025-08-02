using System.Diagnostics;
using CodeChallenge.Models;
using CodeChallenge.Repos;
using CodeChallenge.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  IRepository _repository;

        public HomeController(ILogger<HomeController> logger)
        {
          _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Providers()
        {
            return View(new SearchProvider());
        }

        [HttpPost]
        public IActionResult Providers(string practiceName)
        {
            InitRepository();

            var sp = new SearchProvider();
            sp.Practice = practiceName;
            sp.Providers = _repository.GetProviders(practiceName);
            return View(sp);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void InitRepository()
        {
            if (_repository == null)
                _repository = new Repository();
        }
    }
}
