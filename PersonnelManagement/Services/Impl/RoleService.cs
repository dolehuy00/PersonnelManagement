using PersonnelManagement.DTO;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Services.Impl
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _repository;
        private readonly RoleMapper _mapper;
        public RoleService(IGenericRepository<Role> repository, RoleMapper mapper)
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
