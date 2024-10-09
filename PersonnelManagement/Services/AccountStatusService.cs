using PersonnelManagement.DTO;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Services
{
    public class AccountStatusService : IAccountStatusService
    {
        private readonly IGenericCurdRepository<AccountStatus> _genericRepo;
        private AccountStatusMapper _statusMapper;

        public AccountStatusService(IGenericCurdRepository<AccountStatus> repository)
        {
            _genericRepo = repository;
            _statusMapper = new AccountStatusMapper();
        }
        public async Task<AccountStatusDTO> Add(AccountStatusDTO accountStatusDTO)
        {
            var status = _statusMapper.ToModel(accountStatusDTO);
            await _genericRepo.AddAsync(status);
            return _statusMapper.ToDTO(status);
        }

        public async Task Delete(long statusId)
        {
            var status = await _genericRepo.GetByIdAsync(statusId) ?? throw new Exception("Status doesn't exist.");
            await _genericRepo.DeleteAsync(status);
        }

        public async Task<AccountStatusDTO> Edit(AccountStatusDTO accountStatusDTO)
        {
            var status = await _genericRepo.GetByIdAsync(accountStatusDTO.Id) ?? throw new Exception("Status does not exist.");
            await _genericRepo.UpdateAsync(status);
            return _statusMapper.ToDTO(status);
        }

        public async Task<AccountStatusDTO> Get(long statusId)
        {
            var status = await _genericRepo.GetByIdAsync(statusId);
            return status == null ? throw new Exception("Status doesn't exist.") : _statusMapper.ToDTO(status);
        }

        public async Task<ICollection<AccountStatusDTO>> GetAll()
        {
            var statuses = await _genericRepo.GetAllAsync();
            return _statusMapper.TolistDTO(statuses);
        }
    }
}
