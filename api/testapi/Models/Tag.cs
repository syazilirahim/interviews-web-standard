namespace testapi.Models
{
    public class Tag
    {
        public int ProdataTagsID { get; set; } // Primary Key
        public int ErrCode { get; set; }
        public int Station { get; set; }

        // Navigation property for many-to-many
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection<TaskTags> TaskTags { get; set; } = new List<TaskTags>();

    }
}
