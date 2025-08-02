using CodeChallenge.Models;

namespace CodeChallenge.Repos
{
    public interface IRepository
    {
        List<Provider> GetProviders(string practice);
        List<string> GetPatientByDoctor(string doctor);
        List<Patient> GetPatientsByDoctorsAndAtHospital(string doctor);
        List<string> GetPatientsWithoutTreatment();

        bool AddTreatment(Treatment treatment);

        void EditTreatment(Treatment treatment);
    }
}
