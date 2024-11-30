using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class AccountMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;
        public AccountMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Account, AccountDTO>()
                    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                    .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Fullname))
                    .ForMember(dest => dest.EmployeeImage, opt => opt.MapFrom(src => src.Employee.Image))
                    .ForMember(dest => dest.LeaderOfDepartments, opt =>
                        opt.MapFrom(src => src.Employee.LeaderOfDepartments != null
                            ? src.Employee.LeaderOfDepartments.Select(ld => new
                            {
                                ld.Id,
                                ld.Name
                            }).ToList()
                            : null
                        )
                    );
            }).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<AccountDTO, Account>()).CreateMapper();
        }

        public AccountDTO ToDTO(Account account)
        {
            return mapperToDTO.Map<AccountDTO>(account);
        }

        public Account ToModel(AccountDTO accountDTO)
        {
            return mapperToEntity.Map<Account>(accountDTO);
        }

        public ICollection<AccountDTO> TolistDTO(ICollection<Account> account)
        {
            return mapperToDTO.Map<ICollection<AccountDTO>>(account);
        }

        public ICollection<Account> ToListModel(ICollection<AccountDTO> accountDTO)
        {
            return mapperToEntity.Map<ICollection<Account>>(accountDTO);
        }
    }
}
