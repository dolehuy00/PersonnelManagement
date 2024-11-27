namespace PersonnelManagement.DTO
{
    public class SalaryHistoryDTO
    {
        public long Id { get; set; }
        public double BasicSalary { get; set; }
        public double BonusSalary { get; set; }
        public string? Detail { get; set; }
        public double Penalty { get; set; }
        public double Tax { get; set; }
        public DateTime Date { get; set; }
        public long EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
    }
}
