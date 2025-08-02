using CsvHelper.Configuration.Attributes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodeChallenge.Models
{
    public class Hospital
    {
        [Name("Name")]
        public string Name { get; set; }

        [Name("Identity")]
        public string Identity { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Identity);
        }

    }
}
