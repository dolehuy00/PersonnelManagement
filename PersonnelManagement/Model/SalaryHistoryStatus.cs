﻿namespace PersonnelManagement.Model
{
    public class SalaryHistoryStatus
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<SalaryHistory>? SalaryHistories { get; set; }
    }
}