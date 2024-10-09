using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class EmployeeStatusMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;

        public EmployeeStatusMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeStatus, EmployeeStatusDTO>()).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeStatusDTO, EmployeeStatus>()).CreateMapper();
        }

        public EmployeeStatusDTO ToDTO(EmployeeStatus status)
        {
            return mapperToDTO.Map<EmployeeStatusDTO>(status);
        }

        public EmployeeStatus ToModel(EmployeeStatusDTO statusDTO)
        {
            return mapperToEntity.Map<EmployeeStatus>(statusDTO);
        }

        public ICollection<EmployeeStatusDTO> TolistDTO(ICollection<EmployeeStatus> status)
        {
            return mapperToDTO.Map<ICollection<EmployeeStatusDTO>>(status);
        }

        public ICollection<EmployeeStatus> ToListModel(ICollection<EmployeeStatusDTO> statusDTOs)
        {
            return mapperToEntity.Map<ICollection<EmployeeStatus>>(statusDTOs);
        }
    }
}
