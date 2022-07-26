using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Token.Examples.SDK.Models
{

    public class Person
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
