using PersonnelManagement.DTO.Filter;
using PersonnelManagement.DTO;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Services
{
    public class DeptAssignmentService : IDeptAssignmentService
    {
        private IDeptAssignmentRepository _deptAssignmentRepo;
        private DeptAssignmentMapper _deptAssignmentMapper;

        public DeptAssignmentService(IDeptAssignmentRepository deptAssignmentRepo, DeptAssignmentMapper deptAssignmentMapper)
        {

            _deptAssignmentRepo = deptAssignmentRepo ?? throw new ArgumentNullException(nameof(deptAssignmentRepo));
            _deptAssignmentMapper = deptAssignmentMapper;
        }

        public async Task<DeptAssignmentDTO> Add(DeptAssignmentDTO deptAssignmentDTO)
        {
            var addDeptAssignment = await _deptAssignmentMapper.ToModel(deptAssignmentDTO);
            await _deptAssignmentRepo.AddAsync(addDeptAssignment);
            return _deptAssignmentMapper.ToDTO(addDeptAssignment);
        }

        public async Task Delete(long deptAssignmentId)
        {
            if (deptAssignmentId > 0)
            {
                await _deptAssignmentRepo.DeleteAsync(deptAssignmentId);
            }
            else
            {
                throw new Exception("Id is non-valid!");
            }
        }

        public async Task<string[]> DeleteMany(long[] deptAssignmentIds)
        {
            string[] messages = new string[deptAssignmentIds.Length];
            for (var i = 0; i < deptAssignmentIds.Length; i++)
            {
                var deptAssignment = await _deptAssignmentRepo.GetByIdAsync(deptAssignmentIds[i]);
                if (deptAssignment == null)
                {
                    messages[i] = $"Can't delete deptAssignment id = {deptAssignmentIds[i]}. Employee doesn't exist.";
                }
                else
                {
                    await _deptAssignmentRepo.DeleteAsync(deptAssignment);
                    messages[i] = $"Delete deptAssignment id = {deptAssignmentIds[i]} successfully.";
                }
            }
            return messages;
        }

        public async Task<DeptAssignmentDTO> Edit(DeptAssignmentDTO deptAssignmentDTO)
        {
            //var exist = await _deptAssignmentRepo.ExistAsync(deptAssignmentDTO.Id);
            //if (!exist)
            //{
            //    throw new Exception("DeptAssignment does not exist.");
            //}
            var deptAssignment = await _deptAssignmentMapper.ToModel(deptAssignmentDTO);
            await _deptAssignmentRepo.UpdateAsync(deptAssignment);
            return _deptAssignmentMapper.ToDTO(deptAssignment);
        }

        public async Task<DeptAssignmentDTO?> Get(long deptAssignmentId)
        {
            var deptAssignmentResutl = await _deptAssignmentRepo.GetByIdAsync(deptAssignmentId);
            if (deptAssignmentResutl == null)
            {
                throw new Exception("DeptAssignment does not exist.");
            }
            return _deptAssignmentMapper.ToDTO(deptAssignmentResutl);
        }

        public async Task<ICollection<DeptAssignmentDTO>> GetAll()
        {
            ICollection<DeptAssignment> results = await _deptAssignmentRepo.GetAllAsync();
            return _deptAssignmentMapper.TolistDTO(results);
        }

        public async Task<(ICollection<DeptAssignmentDTO>, int totalPages, int totalRecords)> FilterAsync(DeptAssignmentFilterDTO deptAssignmentFilter)
        {
            try
            {
                var (deptAssignments, totalPage, totalRecords) = await _deptAssignmentRepo.FilterAsync(deptAssignmentFilter);
                return (_deptAssignmentMapper.TolistDTO(deptAssignments), totalPage, totalRecords);
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
