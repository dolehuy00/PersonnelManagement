using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;
using System.Linq.Expressions;

namespace PersonnelManagement.Services.Impl
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

        public async Task<ICollection<DeptAssignmentDTO>> AddMany(List<DeptAssignmentDTO> deptAssignmentDTOs)
        {
            List<DeptAssignmentDTO> results = new List<DeptAssignmentDTO>();
            // Thêm các tác vụ vào danh sách
            foreach (DeptAssignmentDTO deptAssignmentDTO in deptAssignmentDTOs)
            {
                deptAssignmentDTO.Id = 0;
                var result = await this.Add(deptAssignmentDTO);
                results.Add(result);
            }

            return results;

        }

        public async Task<ICollection<DeptAssignmentDTO>> EditManyByProjectId(long projectId, List<DeptAssignmentDTO> deptAssignmentDTOs)
        {
            await _deptAssignmentRepo.DeleteByProjectIdAsync(projectId);
            if (deptAssignmentDTOs.Count != 0)
            {
                return await AddMany(deptAssignmentDTOs);
            }
            return new List<DeptAssignmentDTO>();
        }

        public async Task<ICollection<DeptAssignmentDTO>> SearchIdAsync(long id)
        {
            Expression<Func<DeptAssignment, bool>> exppression = de => de.Id == id;
            Expression<Func<DeptAssignment, object>>[] includes = [de => de.Department!];
            var results = await _deptAssignmentRepo.FindListAsync(exppression, includes);
            return _deptAssignmentMapper.TolistDTO(results);
        }
    }
}
