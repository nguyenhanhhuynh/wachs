using CsvHelper.Configuration.Attributes;

namespace CodeChallenge.Models
{
    public class Provider
    {
        [Name("Name")]
        public string Name { get; set; }

        [Name("Number")]
        public string Number { get; set; }

        [Name("Hospital")]
        public string Hospital { get; set; }

        [Name("Doctor"), BooleanTrueValues("Yes"), BooleanFalseValues("No")]
        public bool IsDoctor { get; set; }

        public  bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Number);
        }
    }
}
