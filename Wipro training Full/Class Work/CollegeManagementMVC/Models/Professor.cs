namespace CollegeManagementMVC.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int SubjectId { get; set; }

        public Subject? Subject { get; set; }
    }
}
