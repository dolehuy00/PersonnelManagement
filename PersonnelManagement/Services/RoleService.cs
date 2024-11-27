using PersonnelManagement.DTO;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Services
{
    public class RoleService : IRoleService
    {
        private readonly IGenericCurdRepository<Role> _repository;
        private readonly RoleMapper _mapper;
        public RoleService(IGenericCurdRepository<Role> repository, RoleMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<RoleDTO>> GetAll()
        {
            var roles = await _repository.GetAllAsync();
            return _mapper.ToListDTO(roles);
        }
    }
}
