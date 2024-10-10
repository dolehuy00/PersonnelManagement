using PersonnelManagement.DTO;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Services
{
    public class SalaryHistoryStatusService : ISalaryHistoryStatusService
    {
        private readonly IGenericCurdRepository<SalaryHistoryStatus> _genericRepo;
        private SalaryHistoryStatusMapper _statusMapper;

        public SalaryHistoryStatusService(IGenericCurdRepository<SalaryHistoryStatus> repository)
        {
            _genericRepo = repository;
            _statusMapper = new SalaryHistoryStatusMapper();
        }
        public async Task<SalaryHistoryStatusDTO> Add(SalaryHistoryStatusDTO statusDTO)
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

        public async Task<SalaryHistoryStatusDTO> Edit(SalaryHistoryStatusDTO statusDTO)
        {
            var status = await _genericRepo.GetByIdAsync(statusDTO.Id) ?? throw new Exception("Status does not exist.");
            await _genericRepo.UpdateAsync(status);
            return _statusMapper.ToDTO(status);
        }

        public async Task<SalaryHistoryStatusDTO> Get(long statusId)
        {
            var status = await _genericRepo.GetByIdAsync(statusId);
            return status == null ? throw new Exception("Status doesn't exist.") : _statusMapper.ToDTO(status);
        }

        public async Task<ICollection<SalaryHistoryStatusDTO>> GetAll()
        {
            var statuses = await _genericRepo.GetAllAsync();
            return _statusMapper.TolistDTO(statuses);
        }
    }
}
