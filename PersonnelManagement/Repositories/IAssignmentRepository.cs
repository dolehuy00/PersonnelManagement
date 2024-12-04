using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IAssignmentRepository
    {
        Task<(ICollection<Assignment>, int, int)> FilterAsync(
            string? sortBy, string? status, long? responsiblePesonId, long? projectId,
            long? departmentId, long? deptAssignmentId, int page, int pageSize);
        Task<(ICollection<Assignment> assignments, int, int)> GetPagedListByEmployeeAsync(
            int pageNumber, int pageSize, long employeeId);
    }
}
