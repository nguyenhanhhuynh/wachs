using CodeChallenge.Models;

namespace CodeChallenge.ViewModels
{
    public class SearchPatient
    {
        public string ProviderName { get; set; }
        public List<Patient> Patients { get; set; }
        public List<string> MedicalReferenceNos { get; set; }

        public List<string> PatientNames { get; set; }
    }
}
