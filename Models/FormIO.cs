namespace FormIOProject.Models
{
    public class FormIO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public Guid VersionId { get; set; }
        public bool Latest { get; set; }
        public string FormFields { get; set; }
    }
}
