namespace testapi.Models
{
    public class TaskTags
    {
        public int TaskID { get; set; }
        public TaskItem TaskItem { get; set; }

        public int TagsID { get; set; }
        public Tag Tag { get; set; }
    }
}
