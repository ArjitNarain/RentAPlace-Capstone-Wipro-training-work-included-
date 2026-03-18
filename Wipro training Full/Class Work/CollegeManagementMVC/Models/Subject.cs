using System.Collections.Generic;

namespace CollegeManagementMVC.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<Professor>?  Professors { get; set; }
    }
}
