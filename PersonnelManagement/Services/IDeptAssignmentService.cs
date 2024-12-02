using PersonnelManagement.DTO.Filter;
using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IDeptAssignmentService
    {
        Task<(ICollection<DeptAssignmentDTO>, int totalPages, int totalRecords)> FilterAsync(
            DeptAssignmentFilterDTO deptAssignmentFilter);

        Task<DeptAssignmentDTO> Add(DeptAssignmentDTO deptAssignmentDTO);
        Task<DeptAssignmentDTO> Edit(DeptAssignmentDTO deptAssignmentDTO);
        Task Delete(long deptAssignmentId);
        Task<DeptAssignmentDTO?> Get(long deptAssignmentIds);
        Task<ICollection<DeptAssignmentDTO>> GetAll();
        Task<string[]> DeleteMany(long[] deptAssignmentId);
        Task<ICollection<DeptAssignmentDTO>> AddMany(List<DeptAssignmentDTO> deptAssignmentDTOs);
        Task<ICollection<DeptAssignmentDTO>> EditManyByProjectId(long projectId, List<DeptAssignmentDTO> deptAssignmentDTOs);
    }
}
