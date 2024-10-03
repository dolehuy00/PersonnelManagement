using PersonnelManagement.Model;
using PersonnelManagement.Repositories;
using System.Collections;

namespace PersonnelManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericCurdRepository<Employee> _genericRepository;

        public EmployeeService(IGenericCurdRepository<Employee> repository)
        {
            _genericRepository = repository;
        }
        async Task<IEnumerable> IEmployeeService.GetAllAsync()
        {
            return await _genericRepository.GetAllAsync();
        }
    }
}
