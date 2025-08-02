using CodeChallenge.Repos;
using CodeChallenge.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    public class PatientController : Controller
    {
        private IRepository _repository;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ByDoctor()
        {
            return View(new SearchPatient());
        }

        [HttpPost]
        public IActionResult ByDoctor(string providerName)
        {
            InitRepository();

            var sp = new SearchPatient();
            sp.ProviderName = providerName;
            sp.MedicalReferenceNos = _repository.GetPatientByDoctor(providerName);
            return View(sp);
        }
        public IActionResult ByDoctorAtHospital()
        {
            return View(new SearchPatient());
        }

        [HttpPost]
        public IActionResult ByDoctorAtHospital(string providerName)
        {
            InitRepository();

            var sp = new SearchPatient();
            sp.ProviderName = providerName;
            sp.Patients = _repository.GetPatientsByDoctorsAndAtHospital(providerName);
            return View(sp);
        }

        public IActionResult NoTreatment()
        {
            InitRepository();
            var sp = new SearchPatient();
            sp.PatientNames = _repository.GetPatientsWithoutTreatment();
            return View(sp);
        }

       

        private void InitRepository()
        {
            if (_repository == null)
                _repository = new Repository();
        }
    }
}
