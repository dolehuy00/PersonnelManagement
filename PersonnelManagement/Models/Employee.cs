﻿namespace PersonnelManagement.Model
{
    public class Employee
    {
        public long Id { get; set; }
        public string? Address { get; set; }
        public double BasicSalary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Fullname { get; set; }
        public string? Position { get; set; }
        public DateTime StartDate { get; set; }
        public Account? Account { get; set; }
        public int StatusId { get; set; }
        public required EmployeeStatus Status { get; set; }
        public long? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<SalaryHistory>? SalaryHistories { get; set; }
        public ICollection<Assignment>? Assignments { get; set; }
        public ICollection<Department>? LeaderOfDepartments { get; set; }
    }
}