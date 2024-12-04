using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;
using System.Linq.Expressions;

namespace PersonnelManagement.Services.Impl
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IGenericRepository<Assignment> _genericRepo;
        private readonly AssignmentMapper _mapper;
        private readonly IAssignmentRepository _assignmentRepo;

        public AssignmentService(IGenericRepository<Assignment> repository, IAssignmentRepository assignmentRepository)
        {
            _genericRepo = repository;
            _assignmentRepo = assignmentRepository;
            _mapper = new AssignmentMapper();
        }

        public async Task<AssignmentDTO> Add(AssignmentDTO assignmentDTO)
        {
            var newAssignment = _mapper.ToEntity(assignmentDTO);
            await _genericRepo.AddAsync(newAssignment);
            return _mapper.ToDTO(newAssignment);

        }

        public async Task<AssignmentDTO> Edit(AssignmentDTO assignmentDTO)
        {
            var exist = await _genericRepo.ExistAsync(assignmentDTO.Id);
            if (!exist)
            {
                throw new Exception("Assignment does not exist.");
            }
            var assignment = _mapper.ToEntity(assignmentDTO);
            await _genericRepo.UpdateAsync(assignment);
            return _mapper.ToDTO(assignment);
        }

        public async Task Delete(long assignmentId)
        {
            var assignment = await _genericRepo.GetByIdAsync(assignmentId)
                ?? throw new Exception("Assignment doesn't exist.");
            await _genericRepo.DeleteAsync(assignment);
        }

        public async Task<AssignmentDTO> Get(long assignmentId)
        {
            Expression<Func<Assignment, bool>> exppression = s => s.Id == assignmentId;
            Expression<Func<Assignment, object>>[] includes = [s => s.ResponsiblePeson!];
            var assignment = await _genericRepo.FindOneAsync(exppression, includes);
            return assignment == null
                ? throw new Exception("Assignment doesn't exist.")
                : _mapper.ToDTO(assignment);
        }

        public async Task<ICollection<AssignmentDTO>> GetAll()
        {
            var assignments = await _genericRepo.GetAllAsync();
            return _mapper.TolistDTO(assignments);
        }

        public async Task<string[]> DeleteMany(long[] assignmentIds)
        {
            string[] messages = new string[assignmentIds.Length];
            for (var i = 0; i < assignmentIds.Length; i++)
            {
                var assignment = await _genericRepo.GetByIdAsync(assignmentIds[i]);
                if (assignment == null)
                {
                    messages[i] = $"Can't delete assignment id = {assignmentIds[i]}. Assignment doesn't exist.";
                }
                else
                {
                    await _genericRepo.DeleteAsync(assignment);
                    messages[i] = $"Delete assignment id = {assignmentIds[i]} successfully.";
                }
            }
            return messages;
        }

        public async Task<(ICollection<AssignmentDTO>, int, int)> FilterAsync(AssignmentFilterDTO filter)
        {
            if (filter.Page < 1 || filter.PageSize < 1)
            {
                throw new ArgumentException("Page and PageSize must be >= 1.");
            }
            var (assignments, totalPage, totalRecords) = await _assignmentRepo.FilterAsync(filter.SortBy,
                filter.Status, filter.ResponsiblePesonId, filter.ProjectId, filter.DepartmentId, filter.DeptAssignmentId,
                filter.Page, filter.PageSize);
            return (_mapper.TolistDTO(assignments), totalPage, totalRecords);
        }

        public async Task<AssignmentDTO> GetByEmployee(long assignmentId, long emplyeeId)
        {
            Expression<Func<Assignment, bool>> exppression = s => s.Id == assignmentId && s.ResponsiblePesonId == emplyeeId;
            Expression<Func<Assignment, object>>[] includes = [s => s.ResponsiblePeson!];
            var assignment = await _genericRepo.FindOneAsync(exppression, includes);
            return assignment == null
                ? throw new Exception("Assignment doesn't exist.")
                : _mapper.ToDTO(assignment);
        }

        public async Task<(ICollection<AssignmentDTO>, int, int)> GetPagesByEmployeeAsync(
            int pageNumber, int pageSize, long employeeId)
        {
            var (assignments, totalPage, totalRecords) = await _assignmentRepo.GetPagedListByEmployeeAsync(pageNumber, pageSize, employeeId);
            return (_mapper.TolistDTO(assignments), totalPage, totalRecords);
        }

        public async Task<(ICollection<AssignmentDTO>, int, int)> FilterByUserAsync(AssignmentFilterDTO filter, long userId)
        {
            if (filter.Page < 1 || filter.PageSize < 1)
            {
                throw new ArgumentException("Page and PageSize must be >= 1.");
            }
            var (assignments, totalPage, totalRecords) = await _assignmentRepo.FilterAsync(filter.SortBy,
                filter.Status, userId, null, null, null, filter.Page, filter.PageSize);
            return (_mapper.TolistDTO(assignments), totalPage, totalRecords);
        }
    }
}
