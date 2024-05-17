using System.ComponentModel.DataAnnotations;

namespace Nexus.Token.Stellar.Examples.Models
{

    public class Person
    {
        [Required]
        public string? Name { get; set; }


        public int Age { get; set; }
    }
}
