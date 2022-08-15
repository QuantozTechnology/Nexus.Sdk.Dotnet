using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Token.Examples.SDK.Models
{

    public class Person
    {
        [Required]
        public string? Name { get; set; }


        public int Age { get; set; }
    }
}
