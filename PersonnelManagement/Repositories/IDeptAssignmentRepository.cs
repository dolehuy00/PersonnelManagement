﻿using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IDeptAssignmentRepository : IGenericCurdRepository<DeptAssignment>
    {
        Task<(ICollection<DeptAssignment>, int, int)> FilterAsync(DeptAssignmentFilterDTO projectFilter);
    }
}
