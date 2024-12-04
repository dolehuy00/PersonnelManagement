using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Enum;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;
using System.Linq.Expressions;

namespace PersonnelManagement.Services.Impl
{
    public class DepartmentService : IDepartmentService
    {

        private IDepartmentRepository _deptRepo;
        private DepartmentMapper _deptMapper;

        public DepartmentService(IDepartmentRepository deptRepo)
        {

            _deptRepo = deptRepo ?? throw new ArgumentNullException(nameof(deptRepo));
            _deptMapper = new DepartmentMapper();
        }

        public async Task<DepartmentDTO> Add(DepartmentDTO departmentDTO)
        {
            Department addDepartment = _deptMapper.ToModel(departmentDTO);
            await _deptRepo.AddAsync(addDepartment);
            return _deptMapper.ToDTO(addDepartment);
        }

        public async Task Delete(long departmentId)
        {
            if (departmentId > 0)
            {
                await _deptRepo.DeleteAsync(departmentId);
            }
            else
            {
                throw new Exception("Id is non-valid!");
            }
        }

        public async Task<string[]> DeleteMany(long[] departmentIds)
        {
            string[] messages = new string[departmentIds.Length];
            for (var i = 0; i < departmentIds.Length; i++)
            {
                var department = await _deptRepo.GetByIdAsync(departmentIds[i]);
                if (department == null)
                {
                    messages[i] = $"Can't delete department id = {departmentIds[i]}. Employee doesn't exist.";
                }
                else
                {
                    await _deptRepo.DeleteAsync(department);
                    messages[i] = $"Delete department id = {departmentIds[i]} successfully.";
                }
            }
            return messages;
        }

        public async Task<DepartmentDTO> Edit(DepartmentDTO departmentDTO)
        {
            //var exist = await _deptRepo.ExistAsync(departmentDTO.Id);
            //if (!exist)
            //{
            //    throw new Exception("Department does not exist.");
            //}
            var department = _deptMapper.ToModel(departmentDTO);
            await _deptRepo.UpdateAsync(department);
            return _deptMapper.ToDTO(department);
        }

        public async Task<DepartmentDTO?> Get(long departmentId)
        {
            var deptResutl = await _deptRepo.GetByIdIncludeLeaderAsync(departmentId);
            if (deptResutl == null)
            {
                throw new Exception("Department does not exist.");
            }
            return _deptMapper.ToDTO(deptResutl);
        }

        public async Task<ICollection<DepartmentDTO>> GetAll()
        {
            ICollection<Department> results = await _deptRepo.GetAllAsync();
            return _deptMapper.TolistDTO(results);
        }

        public async Task<(ICollection<DepartmentDTO>, int totalPages, int totalRecords)> FilterAsync(DepartmentFilterDTO deptFilter)
        {
            try
            {
                var (departments, totalPage, totalRecords) = await _deptRepo.FilterAsync(deptFilter);
                return (_deptMapper.TolistDTO(departments), totalPage, totalRecords);
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

        public async Task<string> changeStatus(long departmentId)
        {
            Department currentDepartment = await _deptRepo.GetByIdAsync(departmentId);
            currentDepartment.Status = currentDepartment.Status.Equals(Status.Lock) ? Status.Active : Status.Lock;
            await _deptRepo.UpdateAsync(currentDepartment);
            return currentDepartment.Status;
        }

        public async Task<bool> IsLeaderOfDepartment(long? departmentId, long leaderId)
        {
            if (departmentId == null) throw new Exception("'departmentId' is requid");
            Expression<Func<Department, bool>> expression = d => d.Id == departmentId && d.LeaderId == leaderId;
            return await _deptRepo.FindOneAsync(expression) != null;
        }
    }
}
