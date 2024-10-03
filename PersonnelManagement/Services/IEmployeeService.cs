using System.Collections;

namespace PersonnelManagement.Services
{
    public interface IEmployeeService
    {
        public Task<IEnumerable> GetAllAsync();
    }
}
