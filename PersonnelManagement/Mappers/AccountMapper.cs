using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class AccountMapper
    {
        private IMapper mapper;

        public AccountMapper()
        {
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Account, AccountDTO>()
                    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                    .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                    .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Fullname))
                    .ReverseMap();
            }).CreateMapper();
        }

        public AccountDTO ToDTO(Account account)
        {
            return mapper.Map<AccountDTO>(account);
        }

        public Account ToModel(AccountDTO accountDTO)
        {
            return mapper.Map<Account>(accountDTO);
        }

        public ICollection<AccountDTO> TolistDTO(ICollection<Account> account)
        {
            return mapper.Map<ICollection<AccountDTO>>(account);
        }

        public ICollection<Account> ToListModel(ICollection<AccountDTO> accountDTO)
        {
            return mapper.Map<ICollection<Account>>(accountDTO);
        }
    }
}
