using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;

namespace PersonnelManagement.Services
{
    public interface IAssignmentService
    {
        Task<AssignmentDTO> Add(AssignmentDTO accountDTO);
        Task<AssignmentDTO> Edit(AssignmentDTO accountDTO);
        Task Delete(long accountId);
        Task<AssignmentDTO> Get(long accountId);
        Task<ICollection<AssignmentDTO>> GetAll();
        Task<string[]> DeleteMany(long[] accountId);
        Task<(ICollection<AssignmentDTO>, int, int)> FilterAsync(AssignmentFilterDTO filter);
        Task<AssignmentDTO> GetByEmployee(long assignmentId, long emplyeeId);
        Task<(ICollection<AssignmentDTO>, int, int)> GetPagesByEmployeeAsync(
            int pageNumber, int pageSize, long employeeId);
        Task<(ICollection<AssignmentDTO>, int, int)> FilterByUserAsync(AssignmentFilterDTO filter, long userId);
        Task ChangeStatusByUser(long id, string status, long userId);
    }
}
