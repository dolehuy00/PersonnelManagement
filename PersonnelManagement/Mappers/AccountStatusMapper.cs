using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class AccountStatusMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;

        public AccountStatusMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<AccountStatus, AccountStatusDTO>()).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<AccountStatusDTO, AccountStatus>()).CreateMapper();
        }

        public AccountStatusDTO ToDTO(AccountStatus status)
        {
            return mapperToDTO.Map<AccountStatusDTO>(status);
        }

        public AccountStatus ToModel(AccountStatusDTO statusDTO)
        {
            return mapperToEntity.Map<AccountStatus>(statusDTO);
        }

        public ICollection<AccountStatusDTO> TolistDTO(ICollection<AccountStatus> status)
        {
            return mapperToDTO.Map<ICollection<AccountStatusDTO>>(status);
        }

        public ICollection<AccountStatus> ToListModel(ICollection<AccountStatusDTO> statusDTOs)
        {
            return mapperToEntity.Map<ICollection<AccountStatus>>(statusDTOs);
        }
    }
}
