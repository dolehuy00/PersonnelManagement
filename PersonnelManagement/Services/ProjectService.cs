using PersonnelManagement.DTO.Filter;
using PersonnelManagement.DTO;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _projectRepo;
        private ProjectMapper _projectMapper;

        public ProjectService(IProjectRepository projectRepo)
        {

            _projectRepo = projectRepo ?? throw new ArgumentNullException(nameof(projectRepo));
            _projectMapper = new ProjectMapper();
        }

        public async Task<ProjectDTO> Add(ProjectDTO projectDTO)
        {
            Project addProject = _projectMapper.ToModel(projectDTO);
            await _projectRepo.AddAsync(addProject);
            return _projectMapper.ToDTO(addProject);
        }

        public async Task Delete(long projectId)
        {
            if (projectId > 0)
            {
                await _projectRepo.DeleteAsync(projectId);
            }
            else
            {
                throw new Exception("Id is non-valid!");
            }
        }

        public async Task<string[]> DeleteMany(long[] projectIds)
        {
            string[] messages = new string[projectIds.Length];
            for (var i = 0; i < projectIds.Length; i++)
            {
                var project = await _projectRepo.GetByIdAsync(projectIds[i]);
                if (project == null)
                {
                    messages[i] = $"Can't delete project id = {projectIds[i]}. Employee doesn't exist.";
                }
                else
                {
                    await _projectRepo.DeleteAsync(project);
                    messages[i] = $"Delete project id = {projectIds[i]} successfully.";
                }
            }
            return messages;
        }

        public async Task<ProjectDTO> Edit(ProjectDTO projectDTO)
        {
            //var exist = await _projectRepo.ExistAsync(projectDTO.Id);
            //if (!exist)
            //{
            //    throw new Exception("Project does not exist.");
            //}
            var project = _projectMapper.ToModel(projectDTO);
            await _projectRepo.UpdateAsync(project);
            return _projectMapper.ToDTO(project);
        }

        public async Task<ProjectDTO?> Get(long projectId)
        {
            var projectResutl = await _projectRepo.GetByIdAsync(projectId);
            if (projectResutl == null)
            {
                throw new Exception("Project does not exist.");
            }
            return _projectMapper.ToDTO(projectResutl);
        }

        public async Task<ICollection<ProjectDTO>> GetAll()
        {
            ICollection<Project> results = await _projectRepo.GetAllAsync();
            return _projectMapper.TolistDTO(results);
        }

        public async Task<(ICollection<ProjectDTO>, int totalPages, int totalRecords)> FilterAsync(ProjectFilterDTO projectFilter)
        {
            try
            {
                var (projects, totalPage, totalRecords) = await _projectRepo.FilterAsync(projectFilter);
                return (_projectMapper.TolistDTO(projects), totalPage, totalRecords);
            }
            catch (Exception ex)
            {
                // Ghi log thông tin lỗi (sử dụng ILogger nếu có)
                Console.WriteLine($"Lỗi: {ex.Message}");
                Console.WriteLine($"Chi tiết: {ex.StackTrace}");

                // Ném lại exception để bảo toàn ngữ cảnh
                throw;
            }
        }
    }
}
