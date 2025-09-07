namespace testapi.Models
{
    public class TaskItem
    {
        public int ProdataID { get; set; }
        public string? Register { get; set; }
        public string? Diagnostic { get; set; }
        public string? Packaging { get; set; }
        // Many-to-many relationship
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<TaskTags> TaskTags { get; set; } = new List<TaskTags>();
    }
}
