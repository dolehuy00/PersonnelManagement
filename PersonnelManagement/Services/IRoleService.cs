using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IRoleService
    {
        Task<ICollection<RoleDTO>> GetAll();
    }
}
