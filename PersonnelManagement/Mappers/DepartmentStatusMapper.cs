using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class DepartmentStatusMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;

        public DepartmentStatusMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<DepartmentStatus, DepartmentStatusDTO>()).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<DepartmentStatusDTO, DepartmentStatus>()).CreateMapper();
        }

        public DepartmentStatusDTO ToDTO(DepartmentStatus status)
        {
            return mapperToDTO.Map<DepartmentStatusDTO>(status);
        }

        public DepartmentStatus ToModel(DepartmentStatusDTO statusDTO)
        {
            return mapperToEntity.Map<DepartmentStatus>(statusDTO);
        }

        public ICollection<DepartmentStatusDTO> TolistDTO(ICollection<DepartmentStatus> status)
        {
            return mapperToDTO.Map<ICollection<DepartmentStatusDTO>>(status);
        }

        public ICollection<DepartmentStatus> ToListModel(ICollection<DepartmentStatusDTO> statusDTOs)
        {
            return mapperToEntity.Map<ICollection<DepartmentStatus>>(statusDTOs);
        }
    }
}
