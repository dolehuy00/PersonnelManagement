using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IAssignmentStatusService
    {
        Task<AssignmentStatusDTO> Add(AssignmentStatusDTO statusDTO);
        Task<AssignmentStatusDTO> Edit(AssignmentStatusDTO statusDTO);
        Task Delete(long statusId);
        Task<AssignmentStatusDTO> Get(long statusId);
        Task<ICollection<AssignmentStatusDTO>> GetAll();
    }
}
