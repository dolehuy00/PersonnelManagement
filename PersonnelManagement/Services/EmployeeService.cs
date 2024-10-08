using PersonnelManagement.DTO;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericCurdRepository<Employee> _genericEmplRepo;
        private EmployeeMapper _emplMapper;
        private IEmployeeRepository _emplRepo;

        public EmployeeService(IGenericCurdRepository<Employee> repository, IEmployeeRepository employeeRepository)
        {
            _genericEmplRepo = repository;
            _emplRepo = employeeRepository;
            _emplMapper = new EmployeeMapper();
        }

        public async Task<EmployeeDTO> Add(EmployeeDTO employeeDTO)
        {
            var newEmployee = _emplMapper.ToModel(employeeDTO);
            await _genericEmplRepo.AddAsync(newEmployee);
            return _emplMapper.ToDTO(newEmployee);

        }

        public async Task<EmployeeDTO> Edit(EmployeeDTO employeeDTO)
        {
            var exist = await _genericEmplRepo.ExistAsync(employeeDTO.Id);
            if (!exist)
            {
                throw new Exception("Employee does not exist.");
            }
            var employee = _emplMapper.ToModel(employeeDTO);
            await _emplRepo.UpdateAsync(employee);
            return _emplMapper.ToDTO(employee);
        }

        public async Task Delete(long employeeId)
        {
            var employee = await _genericEmplRepo.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new Exception("Employee doesn't exist.");
            }
            await _genericEmplRepo.DeleteAsync(employee);
        }

        public async Task<EmployeeDTO> Get(long employeeId)
        {
            var employee = await _genericEmplRepo.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new Exception("Employee doesn't exist.");
            }
            return _emplMapper.ToDTO(employee);
        }

        public async Task<ICollection<EmployeeDTO>> GetAll()
        {
            var employees = await _genericEmplRepo.GetAllAsync();
            return _emplMapper.TolistDTO(employees);
        }

        public async Task<string[]> DeleteMany(long[] employeeIds)
        {
            string[] messages = new string[employeeIds.Length];
            for (var i = 0; i < employeeIds.Length; i++)
            {
                var employee = await _genericEmplRepo.GetByIdAsync(employeeIds[i]);
                if (employee == null)
                {
                    messages[i] = $"Can't delete employee id = {employeeIds[i]}. Employee doesn't exist.";
                }
                else
                {
                    await _genericEmplRepo.DeleteAsync(employee);
                    messages[i] = $"Delete employee id = {employeeIds[i]} successfully.";
                }
            }
            return messages;
        }

        public async Task<(ICollection<EmployeeDTO>, int totalPages, int totalRecords)> GetPagesAsync(
            int pageNumber, int pageSize)
        {
            var (employees, totalPage, totalRecords) = await _emplRepo.GetPagedListAsync(pageNumber, pageSize);
            return (_emplMapper.TolistDTO(employees), totalPage, totalRecords);
        }

        public async Task<(ICollection<EmployeeDTO>, int totalPages, int totalRecords)> FilterAsync(EmployeeFilterDTO filter)
        {
            if (filter.Page < 1 || filter.PageSize < 1)
            {
                throw new ArgumentException("Page and PageSize must be >= 1.");
            }
            var (employees, totalPage, totalRecords) = await _emplRepo.FilterAsync(filter.NameOrId,
                filter.Address, filter.FromDoB, filter.ToDoB, filter.FromSalary, filter.ToSalary,
                filter.Position, filter.FromStartDate, filter.ToStartDate, filter.DepartmentId,
                filter.StatusId, filter.SortBy, filter.Page, filter.PageSize);
            return (_emplMapper.TolistDTO(employees), totalPage, totalRecords);
        }
    }
}
