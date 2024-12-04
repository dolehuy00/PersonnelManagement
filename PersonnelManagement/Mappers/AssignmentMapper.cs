using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class AssignmentMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;

        public AssignmentMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Assignment, AssignmentDTO>()
                    .ForMember(
                        dest => dest.ResponsiblePesonName,
                        opt => opt.MapFrom(src => src.ResponsiblePeson == null ? null : src.ResponsiblePeson.Fullname))
                    .ForMember(
                        dest => dest.ProjectId,
                        opt => opt.MapFrom(src => src.DeptAssignment.Project.Id))
                    .ForMember(
                        dest => dest.ProjectName,
                        opt => opt.MapFrom(src => src.DeptAssignment.Project == null ? null : src.DeptAssignment.Project.Name));
            }).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<AssignmentDTO, Assignment>()).CreateMapper();
        }

        public AssignmentDTO ToDTO(Assignment assignment)
        {
            return mapperToDTO.Map<AssignmentDTO>(assignment);
        }

        public Assignment ToEntity(AssignmentDTO assignmentDTO)
        {
            return mapperToEntity.Map<Assignment>(assignmentDTO);
        }

        public ICollection<AssignmentDTO> TolistDTO(ICollection<Assignment> assignments)
        {
            return mapperToDTO.Map<ICollection<AssignmentDTO>>(assignments);
        }

        public ICollection<Assignment> ToListEntity(ICollection<AssignmentDTO> assignmentDTOs)
        {
            return mapperToEntity.Map<ICollection<Assignment>>(assignmentDTOs);
        }
    }
}
