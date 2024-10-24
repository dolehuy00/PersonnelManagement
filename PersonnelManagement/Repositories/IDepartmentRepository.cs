﻿using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IDepartmentRepository : IGenericCurdRepository<Department>
    {
        Task<(ICollection<Department>, int, int)> FilterAsync(DepartmentFilterDTO departmentFilter);

    }
}
