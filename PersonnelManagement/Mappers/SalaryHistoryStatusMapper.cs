using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class SalaryHistoryStatusMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;

        public SalaryHistoryStatusMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<SalaryHistoryStatus, SalaryHistoryStatusDTO>()).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<SalaryHistoryStatusDTO, SalaryHistoryStatus>()).CreateMapper();
        }

        public SalaryHistoryStatusDTO ToDTO(SalaryHistoryStatus status)
        {
            return mapperToDTO.Map<SalaryHistoryStatusDTO>(status);
        }

        public SalaryHistoryStatus ToModel(SalaryHistoryStatusDTO statusDTO)
        {
            return mapperToEntity.Map<SalaryHistoryStatus>(statusDTO);
        }

        public ICollection<SalaryHistoryStatusDTO> TolistDTO(ICollection<SalaryHistoryStatus> status)
        {
            return mapperToDTO.Map<ICollection<SalaryHistoryStatusDTO>>(status);
        }

        public ICollection<SalaryHistoryStatus> ToListModel(ICollection<SalaryHistoryStatusDTO> statusDTOs)
        {
            return mapperToEntity.Map<ICollection<SalaryHistoryStatus>>(statusDTOs);
        }
    }
}
