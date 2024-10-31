using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class ProjectMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;
        public ProjectMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Project, ProjectDTO>()
                   .ForMember(dest => dest.DeptAssignmentDTOs,
                  opt => opt.MapFrom(src => src.DeptAssignments));
            }).CreateMapper();

            mapperToEntity = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProjectDTO, Project>()
                   .ForMember(dest => dest.DeptAssignments, opt => opt.Ignore()); // Bỏ qua nếu không cần ánh xạ ngược từ DTO sang Entity
            }).CreateMapper();
        }

        public ProjectDTO ToDTO(Project project)
        {
            return mapperToDTO.Map<ProjectDTO>(project);
        }

        public Project ToModel(ProjectDTO projectDTO)
        {
            return mapperToEntity.Map<Project>(projectDTO);
        }

        public ICollection<ProjectDTO> TolistDTO(ICollection<Project> project)
        {
            return mapperToDTO.Map<ICollection<ProjectDTO>>(project);
        }

        public ICollection<Project> ToListModel(ICollection<ProjectDTO> projectDTO)
        {
            return mapperToEntity.Map<ICollection<Project>>(projectDTO);
        }
    }
}
