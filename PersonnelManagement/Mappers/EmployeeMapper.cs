using AutoMapper;
using PersonnelManagement.DTO;
using PersonnelManagement.Model;

namespace PersonnelManagement.Mappers
{
    public class EmployeeMapper
    {
        private IMapper mapperToDTO;
        private IMapper mapperToEntity;

        public EmployeeMapper()
        {
            mapperToDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDTO>()
                    .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                    .ForMember(
                        dest => dest.DepartmentName,
                        opt => opt.MapFrom(src => src.Department == null ? "No" : src.Department.Name));
            }).CreateMapper();
            mapperToEntity = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, Employee>()).CreateMapper();
        }

        public EmployeeDTO ToDTO(Employee employee)
        {
            return mapperToDTO.Map<EmployeeDTO>(employee);
        }

        public Employee ToModel(EmployeeDTO employeeDTO)
        {
            return mapperToEntity.Map<Employee>(employeeDTO);
        }

        public ICollection<EmployeeDTO> TolistDTO(ICollection<Employee> employees)
        {
            return mapperToDTO.Map<ICollection<EmployeeDTO>>(employees);
        }

        public ICollection<Employee> ToListModel(ICollection<EmployeeDTO> employeeDTOs)
        {
            return mapperToEntity.Map<ICollection<Employee>>(employeeDTOs);
        }
    }
}
