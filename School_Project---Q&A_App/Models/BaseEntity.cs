namespace School_Project___Q_A_App.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Is_Active { get; set; }
    }
}
