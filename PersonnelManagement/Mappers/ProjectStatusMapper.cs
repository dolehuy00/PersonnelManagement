using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class ProjectStatusMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;

        public ProjectStatusMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<ProjectStatus, ProjectStatusDTO>()).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<ProjectStatusDTO, ProjectStatus>()).CreateMapper();
        }

        public ProjectStatusDTO ToDTO(ProjectStatus status)
        {
            return mapperToDTO.Map<ProjectStatusDTO>(status);
        }

        public ProjectStatus ToModel(ProjectStatusDTO statusDTO)
        {
            return mapperToEntity.Map<ProjectStatus>(statusDTO);
        }

        public ICollection<ProjectStatusDTO> TolistDTO(ICollection<ProjectStatus> status)
        {
            return mapperToDTO.Map<ICollection<ProjectStatusDTO>>(status);
        }

        public ICollection<ProjectStatus> ToListModel(ICollection<ProjectStatusDTO> statusDTOs)
        {
            return mapperToEntity.Map<ICollection<ProjectStatus>>(statusDTOs);
        }
    }
}
