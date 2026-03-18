using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeManagementMVC.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Foreign Keys
        public int SubjectId { get; set; }
        public int ParentId { get; set; }

        // Navigation Properties
        public Subject Subject { get; set; }
        public Parent Parent { get; set; }
    }
}
