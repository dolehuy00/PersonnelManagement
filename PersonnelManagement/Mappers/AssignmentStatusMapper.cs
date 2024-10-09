using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class AssignmentStatusMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;

        public AssignmentStatusMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<AssignmentStatus, AssignmentStatusDTO>()).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<AssignmentStatusDTO, AssignmentStatus>()).CreateMapper();
        }

        public AssignmentStatusDTO ToDTO(AssignmentStatus status)
        {
            return mapperToDTO.Map<AssignmentStatusDTO>(status);
        }

        public AssignmentStatus ToModel(AssignmentStatusDTO statusDTO)
        {
            return mapperToEntity.Map<AssignmentStatus>(statusDTO);
        }

        public ICollection<AssignmentStatusDTO> TolistDTO(ICollection<AssignmentStatus> status)
        {
            return mapperToDTO.Map<ICollection<AssignmentStatusDTO>>(status);
        }

        public ICollection<AssignmentStatus> ToListModel(ICollection<AssignmentStatusDTO> statusDTOs)
        {
            return mapperToEntity.Map<ICollection<AssignmentStatus>>(statusDTOs);
        }
    }
}
