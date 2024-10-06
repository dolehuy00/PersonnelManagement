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

        async Task<EmployeeDTO> IEmployeeService.Add(EmployeeDTO employeeDTO)
        {
            var newEmployee = _emplMapper.ToModel(employeeDTO);
            await _genericEmplRepo.AddAsync(newEmployee);
            return _emplMapper.ToDTO(newEmployee);

        }

        async Task<EmployeeDTO> IEmployeeService.Edit(EmployeeDTO employeeDTO)
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

        async Task IEmployeeService.Delete(long employeeId)
        {
            var employee = await _genericEmplRepo.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new Exception("Employee doesn't exist.");
            }
            await _genericEmplRepo.DeleteAsync(employee);
        }

        async Task<EmployeeDTO> IEmployeeService.Get(long employeeId)
        {
            var employee = await _genericEmplRepo.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new Exception("Employee doesn't exist.");
            }
            return _emplMapper.ToDTO(employee);
        }

        async Task<ICollection<EmployeeDTO>> IEmployeeService.GetAll()
        {
            var employees = await _genericEmplRepo.GetAllAsync();
            return _emplMapper.TolistDTO(employees);
        }

        async Task<string[]> IEmployeeService.DeleteMany(long[] employeeIds)
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

        async Task<(ICollection<EmployeeDTO>, int totalPages, int totalRecords)> IEmployeeService.GetPagedListWithTotalPagesAsync(int pageNumber, int pageSize)
        {
            var (employees, totalPage, totalRecords) = await _emplRepo.GetPagedListAsync(pageNumber, pageSize);
            return (_emplMapper.TolistDTO(employees), totalPage, totalRecords);
        }
    }
}
