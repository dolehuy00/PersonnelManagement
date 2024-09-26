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
        public int StatusId { get; set; }
        public required AssignmentStatus Status { get; set; }
        public long ResponsiblePesonId { get; set; }
        public required Employee ResponsiblePeson { get; set; }
        public long DeptAssignmentId { get; set; }
        public required DeptAssignment DeptAssignment { get; set; }
    }
}
