namespace School_Project___Q_A_App.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public Post Post { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
