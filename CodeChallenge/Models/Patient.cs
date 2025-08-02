using CsvHelper.Configuration.Attributes;

namespace CodeChallenge.Models
{
    public class Patient
    {
        [Name("Patient Name")]
        public string PatientName { get; set; }

        [Name("Medical Reference Number")]
        public string MedicalReferenceNo { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(PatientName) 
                && !string.IsNullOrWhiteSpace(MedicalReferenceNo)
                && PatientName.Contains(" ");
        }
    }
}
