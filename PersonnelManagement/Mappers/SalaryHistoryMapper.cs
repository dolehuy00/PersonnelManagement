using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class SalaryHistoryMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;

        public SalaryHistoryMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SalaryHistory, SalaryHistoryDTO>()
                    .ForMember(
                        dest => dest.EmployeeName,
                        opt => opt.MapFrom(src => src.Employee == null ? "Unknow" : src.Employee.Fullname));
            }).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<SalaryHistoryDTO, SalaryHistory>()).CreateMapper();
        }

        public SalaryHistoryDTO ToDTO(SalaryHistory salaryHistory)
        {
            return mapperToDTO.Map<SalaryHistoryDTO>(salaryHistory);
        }

        public SalaryHistory ToEntity(SalaryHistoryDTO salaryHistoryDTO)
        {
            return mapperToEntity.Map<SalaryHistory>(salaryHistoryDTO);
        }

        public ICollection<SalaryHistoryDTO> TolistDTO(ICollection<SalaryHistory> salaryHistorys)
        {
            return mapperToDTO.Map<ICollection<SalaryHistoryDTO>>(salaryHistorys);
        }

        public ICollection<SalaryHistory> ToListEntity(ICollection<SalaryHistoryDTO> salaryHistoryDTOs)
        {
            return mapperToEntity.Map<ICollection<SalaryHistory>>(salaryHistoryDTOs);
        }
    }
}
