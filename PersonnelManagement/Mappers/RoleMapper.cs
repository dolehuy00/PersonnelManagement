using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class RoleMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;

        public RoleMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, Role>()).CreateMapper();
        }

        public RoleDTO ToDTO(Role role)
        {
            return mapperToDTO.Map<RoleDTO>(role);
        }

        public Role ToModel(RoleDTO roleDTO)
        {
            return mapperToEntity.Map<Role>(roleDTO);
        }

        public ICollection<RoleDTO> ToListDTO(ICollection<Role> roles)
        {
            return mapperToDTO.Map<ICollection<RoleDTO>>(roles);
        }

        public ICollection<Role> ToListModel(ICollection<RoleDTO> roleDTOs)
        {
            return mapperToEntity.Map<ICollection<Role>>(roleDTOs);
        }
    }
}
