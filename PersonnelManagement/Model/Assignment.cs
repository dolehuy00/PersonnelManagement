namespace PersonnelManagement.Model
{
    public class Assignment
    {
        public long Id { get; set; }
        public string? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? Detail { get; set; }
        public required string Name { get; set; }
        public int PriotityLevel { get; set; }
        public AssignmentStatus Status { get; set; }
        public required Employee ResponsiblePeson { get; set; }
        public required Project Project { get; set; }
    }
}
