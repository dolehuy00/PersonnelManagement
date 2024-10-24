using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IAssignmentRepository
    {
        Task<(ICollection<Assignment>, int, int)> FilterAsync(
            string? sortBy, string? status, long? responsiblePesonId, long? projectId,
            long? departmentId, int page, int pageSize);
        Task<Assignment?> GetByEmployeeAsync(long assignmentId, long emplyeeId);
        Task<Assignment?> GetFullInforAsync(long id);
        Task<ICollection<Assignment>> GetFullInforAsync();
        Task<(ICollection<Assignment>, int, int)> GetPagedListAsync(int pageNumber, int pageSize);
        Task<(ICollection<Assignment> assignments, int, int)> GetPagedListByEmployeeAsync(
            int pageNumber, int pageSize, long employeeId);
    }
}
