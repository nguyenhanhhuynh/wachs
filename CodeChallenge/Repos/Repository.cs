using CodeChallenge.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CodeChallenge.Repos
{
    public class Repository : IRepository
    {
        List<Provider> _providers;
        List<Hospital> _hospitals;
        List<Patient> _patients;
        List<Treatment> _treatments;

        const string PATH = "Data";
        
        private string _path;
        public Repository() 
        {
            _path = Path.Combine(Directory.GetCurrentDirectory(), PATH);
        }

        public List<Provider> GetProviders(string practice)
        {
            LoadAllProviders();
            return _providers.Where(x => string.Equals(x.Hospital, practice)).ToList();
        }

        public List<string> GetPatientByDoctor(string doctor)
        {
            LoadAllProviders();
            var provider = _providers.FirstOrDefault(x => string.Equals(x.Name, doctor));
            
            if (provider == null || !provider.IsDoctor)
            {
                return new List<string>();
            }
           
            LoadAllTreatments();
            var treatments = _treatments.Where(x => string.Equals(x.Provider, doctor)).ToList();
            return treatments.Select(x => x.Patient).ToList();
        }

        public List<Patient> GetPatientsByDoctorsAndAtHospital(string doctor)
        {
            LoadAllTreatments();
            var treatments = _treatments.Where(x => string.Equals(x.Provider, doctor) && string.IsNullOrEmpty(x.DateTimeDischarged)).ToList();
            var patientNos = treatments.Select(x => x.Patient).ToList();
            LoadAllPatients();

            return _patients.Where(x => patientNos.Contains(x.MedicalReferenceNo)).ToList();
        }

        public List<string> GetPatientsWithoutTreatment()
        {
            LoadAllTreatments();
            LoadAllPatients();
            var withoutTreatments = _treatments.Where(x => string.IsNullOrWhiteSpace(x.Provider)).ToList();
            if(withoutTreatments == null)
                return new List<string> { };

            var patientNos = withoutTreatments.Select(x => x.Patient).ToList();
            var patients = _patients.Where(x => patientNos.Contains(x.MedicalReferenceNo));

            return patients.Select(x => x.PatientName).ToList();
        }

        private void LoadAllProviders()
        {            
            var filePath = Path.Combine(_path, "Providers.csv");
            _providers = ReadDataFromCsvFile<Provider>(filePath);
            _providers.RemoveAll(x => !x.IsValid());
        }

        

        private void LoadAllHospitals()
        {
            var filePath = Path.Combine(_path, "Hospitals.csv");
            _hospitals = ReadDataFromCsvFile<Hospital>(filePath);
            _hospitals.RemoveAll(x => !x.IsValid());
        }

        private void LoadAllPatients()
        {
            var filePath = Path.Combine(_path, "Patients.csv");
            _patients = ReadDataFromCsvFile<Patient>(filePath);          
              _patients.RemoveAll(x => !x.IsValid());
        }

       

        private void LoadAllTreatments()
        {
            var filePath = Path.Combine(_path, "Treatments.csv");
            _treatments = ReadDataFromCsvFile<Treatment>(filePath);
            _treatments.RemoveAll(x => !x.IsValid());
        }

        public bool AddTreatment(Treatment treatment)
        {
            var file = Path.Combine(_path, "Treatments.csv");
            if (Directory.Exists(file))
                return false; // should notify some error code

            var configWrite = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };
            using (var stream = File.Open(file, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, configWrite))
            {
                csv.WriteRecord(treatment);
            }
            return true;
        }
        public void EditTreatment(Treatment treatment)
        {
            //To edit treatment then assume that last treatment using PatientName, 
            //Can patient move hospital?
            LoadAllTreatments();
            var existingLastTreatment = _treatments.Last(x => x.Patient == treatment.Patient);
            if (existingLastTreatment == null)
                return;

            existingLastTreatment.Hospital = treatment.Hospital;            
            existingLastTreatment.DateTimeDischarged = treatment.DateTimeDischarged;
            existingLastTreatment.Provider = treatment.Provider;
            existingLastTreatment.Details = treatment.Details;

            var file = Path.Combine(_path, "Treatments.csv");

            using (var writer = new StreamWriter(file))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_treatments);
            }          
        }

        private List<T> ReadDataFromCsvFile<T>(string file)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
           List<T> records = new List<T>();  
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, config))
            {               
                 records = csv.GetRecords<T>().ToList();
            }
            return records;
        }


    }
}
