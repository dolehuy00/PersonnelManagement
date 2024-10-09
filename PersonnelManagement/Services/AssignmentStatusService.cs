using PersonnelManagement.DTO;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Services
{
    public class AssignmentStatusService : IAssignmentStatusService
    {
        private readonly IGenericCurdRepository<AssignmentStatus> _genericRepo;
        private AssignmentStatusMapper _statusMapper;

        public AssignmentStatusService(IGenericCurdRepository<AssignmentStatus> repository)
        {
            _genericRepo = repository;
            _statusMapper = new AssignmentStatusMapper();
        }
        public async Task<AssignmentStatusDTO> Add(AssignmentStatusDTO statusDTO)
        {
            var status = _statusMapper.ToModel(statusDTO);
            await _genericRepo.AddAsync(status);
            return _statusMapper.ToDTO(status);
        }

        public async Task Delete(long statusId)
        {
            var status = await _genericRepo.GetByIdAsync(statusId) ?? throw new Exception("Status doesn't exist.");
            await _genericRepo.DeleteAsync(status);
        }

        public async Task<AssignmentStatusDTO> Edit(AssignmentStatusDTO statusDTO)
        {
            var status = await _genericRepo.GetByIdAsync(statusDTO.Id) ?? throw new Exception("Status does not exist.");
            await _genericRepo.UpdateAsync(status);
            return _statusMapper.ToDTO(status);
        }

        public async Task<AssignmentStatusDTO> Get(long statusId)
        {
            var status = await _genericRepo.GetByIdAsync(statusId);
            return status == null ? throw new Exception("Status doesn't exist.") : _statusMapper.ToDTO(status);
        }

        public async Task<ICollection<AssignmentStatusDTO>> GetAll()
        {
            var statuses = await _genericRepo.GetAllAsync();
            return _statusMapper.TolistDTO(statuses);
        }
    }
}
