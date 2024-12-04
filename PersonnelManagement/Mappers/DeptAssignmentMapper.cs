using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Mappers
{
    public class DeptAssignmentMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;
        private IDepartmentRepository _deptRepo;
        private IProjectRepository _projectRepo;

        public DeptAssignmentMapper(IDepartmentRepository deptRepo, IProjectRepository projectRepo)
        {
            mapperToDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeptAssignment, DeptAssignmentDTO>()
                    .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                    .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                    .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
            }).CreateMapper();

            mapperToEntity = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DeptAssignmentDTO, DeptAssignment>()
                    .ForMember(dest => dest.Project, opt => opt.Ignore());
            }).CreateMapper();
            _projectRepo = projectRepo;
            _deptRepo = deptRepo;
        }

        public DeptAssignmentDTO ToDTO(DeptAssignment deptAssignment)
        {
            return mapperToDTO.Map<DeptAssignmentDTO>(deptAssignment);
        }

        public async Task<DeptAssignment> ToModel(DeptAssignmentDTO deptAssignmentDTO)
        {
            var deptAssignment = mapperToEntity.Map<DeptAssignment>(deptAssignmentDTO);
            if (deptAssignmentDTO.ProjectId != 0)
            {
                var project = await _projectRepo.GetByIdAsync(deptAssignmentDTO.ProjectId);
                if (project != null) { deptAssignment.Project = project; }
            }
            if (deptAssignment.DepartmentId != 0)
            {
                var department = await _deptRepo.GetByIdAsync(deptAssignmentDTO.DepartmentId);
                if (department != null) { deptAssignment.Department = department; }
            }
            return deptAssignment;
        }

        public ICollection<DeptAssignmentDTO> TolistDTO(ICollection<DeptAssignment> deptAssignment)
        {
            return mapperToDTO.Map<ICollection<DeptAssignmentDTO>>(deptAssignment);
        }

        public async Task<ICollection<DeptAssignment>> ToListModel(ICollection<DeptAssignmentDTO> deptAssignmentDTO)
        {
            // Bắt đầu ánh xạ cơ bản
            var deptAssignments = deptAssignmentDTO == null
                ? new List<DeptAssignment>()
                : mapperToEntity.Map<List<DeptAssignment>>(deptAssignmentDTO);

            // Xử lý các thuộc tính phức tạp cần gọi repo, ví dụ: Project
            foreach (var deptAssignment in deptAssignments)
            {
                // Nếu ProjectId khác 0, lấy Project từ repo
                if (deptAssignment.ProjectId != 0)
                {
                    var project = await _projectRepo.GetByIdAsync(deptAssignment.ProjectId);
                    if (project != null) { deptAssignment.Project = project; }
                }
                if (deptAssignment.DepartmentId != 0)
                {
                    var department = await _deptRepo.GetByIdAsync(deptAssignment.DepartmentId);
                    if (department != null) { deptAssignment.Department = department; }
                }
            }
            return deptAssignments;
        }
    }
}
