using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;


namespace CodeChallenge.Models
{
    public class Treatment
    {
        [Name("Details")]
        public string Details { get; set; }


        [Name("Hospital")]
        public string Hospital { get; set; }

        [Name("Provider")]
        public string Provider { get; set; }

        [Name("Patient")]
        public string Patient { get; set; }

    
        [Name("Date/Time Discharged")]
        public string DateTimeDischarged {  get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Hospital)
                && !string.IsNullOrWhiteSpace(Patient);
        }
    }
}
