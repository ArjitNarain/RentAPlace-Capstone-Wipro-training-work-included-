using System.ComponentModel.DataAnnotations;

namespace PartySignupApp.Models
{
    public class Party
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsExternal { get; set; }
    }
}