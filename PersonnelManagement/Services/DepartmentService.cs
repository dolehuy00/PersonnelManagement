using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Services
{
    public class DepartmentService : IDepartmentService
    {

        private IDepartmentRepository _deptRepo;
        private DepartmentMapper _deptMapper ;

        public DepartmentService( IDepartmentRepository deptRepo)        {
            
            _deptRepo = deptRepo ?? throw new ArgumentNullException(nameof(deptRepo));
            _deptMapper = new DepartmentMapper();
        }

        public async Task<(ICollection<DepartmentDTO>, int totalPages, int totalRecords)> FilterAsync(DepartmentFilterDTO deptFilter)
        {
            try {
                var (departments, totalPage, totalRecords) = await _deptRepo.FilterAsync(deptFilter);
                return (_deptMapper.TolistDTO(departments), totalPage, totalRecords);
            } catch (Exception ex) {
                // Ghi log thông tin lỗi (sử dụng ILogger nếu có)
                Console.WriteLine($"Lỗi: {ex.Message}");
                Console.WriteLine($"Chi tiết: {ex.StackTrace}");

                // Ném lại exception để bảo toàn ngữ cảnh
                throw;
            }            
        }
    }
}
