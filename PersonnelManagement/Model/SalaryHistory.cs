namespace PersonnelManagement.Model
{
    public class SalaryHistory
    {
        public long Id { get; set; }
        public string? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public double BasicSalary { get; set; }
        public double BonusSalary { get; set; }
        public string? Detail { get; set; }
        public double Penalty { get; set; }
        public SalaryHistoryStatus Status { get; set; }
        public double Tax { get; set; }
        public required Employee Employee { get; set; }

    }
}
