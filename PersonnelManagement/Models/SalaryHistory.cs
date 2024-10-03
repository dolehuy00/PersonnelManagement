namespace PersonnelManagement.Model
{
    public class SalaryHistory
    {
        public long Id { get; set; }
        public double BasicSalary { get; set; }
        public double BonusSalary { get; set; }
        public string? Detail { get; set; }
        public double Penalty { get; set; }
        public double Tax { get; set; }
        public int StatusId { get; set; }
        public required SalaryHistoryStatus Status { get; set; }
        public long EmployeeId { get; set; }
        public required Employee Employee { get; set; }
    }
}
