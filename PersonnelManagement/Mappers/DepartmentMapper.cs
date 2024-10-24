using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class DepartmentMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;
        public DepartmentMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Department, DepartmentDTO>()
                    .ForMember(dest => dest.LeaderId, opt => opt.MapFrom(src => src.LeaderId))
                    .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.Leader.Fullname));
            }).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<DepartmentDTO, Department>()).CreateMapper();
        }

        public DepartmentDTO ToDTO(Department department)
        {
            return mapperToDTO.Map<DepartmentDTO>(department);
        }

        public Department ToModel(DepartmentDTO departmentDTO)
        {
            return mapperToEntity.Map<Department>(departmentDTO);
        }

        public ICollection<DepartmentDTO> TolistDTO(ICollection<Department> department)
        {
            return mapperToDTO.Map<ICollection<DepartmentDTO>>(department);
        }

        public ICollection<Department> ToListModel(ICollection<DepartmentDTO> departmentDTO)
        {
            return mapperToEntity.Map<ICollection<Department>>(departmentDTO);
        }
    }
}
