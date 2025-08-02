using CodeChallenge.Models;
using CodeChallenge.Repos;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    public class TreatmentController : Controller
    {
        private IRepository _repository;

        private void InitRepository()
        {
            if (_repository == null)
                _repository = new Repository();
        }
        public IActionResult Add()
        {
            var treatment = new Treatment();
            return View(treatment);
        }

        [HttpPost]
        public IActionResult Add(Treatment treament)
        {
            InitRepository();
            _repository.AddTreatment(treament);
            return View();
        }
    }
}
