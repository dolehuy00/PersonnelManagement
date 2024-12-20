﻿using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;

namespace PersonnelManagement.Services
{
    public interface IDepartmentService
    {
        Task<(ICollection<DepartmentDTO>, int totalPages, int totalRecords)> FilterAsync(
            DepartmentFilterDTO departmentFilter);

        Task<DepartmentDTO> Add(DepartmentDTO departmentDTO);
        Task<DepartmentDTO> Edit(DepartmentDTO departmentDTO);
        Task Delete(long departmentId);
        Task<DepartmentDTO?> Get(long departmentId);
        Task<ICollection<DepartmentDTO>> GetAll();
        Task<string[]> DeleteMany(long[] departmentId);
        Task<string> changeStatus(long departmentId);
        Task<bool> IsLeaderOfDepartment(long? departmentId, long leaderId);
    }
}
