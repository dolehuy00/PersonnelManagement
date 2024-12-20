﻿using MovieAppApi.Service;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Enum;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;
using System.Linq.Expressions;

namespace PersonnelManagement.Services.Impl
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeMapper _emplMapper;
        private readonly IEmployeeRepository _emplRepo;
        private readonly TokenService _tokenServ;
        private readonly IStaticFileService _staticFileServ;

        public EmployeeService(IEmployeeRepository employeeRepository, TokenService tokenService, IStaticFileService staticFileServ)
        {
            _emplRepo = employeeRepository;
            _emplMapper = new EmployeeMapper(tokenService);
            _tokenServ = tokenService;
            _staticFileServ = staticFileServ;
        }

        public async Task<EmployeeDTO> Add(EmployeeDTO employeeDTO)
        {
            var newEmployee = _emplMapper.ToModel(employeeDTO);
            await _emplRepo.AddAsync(newEmployee);
            if (employeeDTO.FileImage != null)
            {
                //Generate token for authen server storage file
                var key = _tokenServ.GenerateAccessTokenImgServer();
                // Create file name
                var fileName = $"avatar-user-{newEmployee.Id}.{employeeDTO.FileImage.FileName.Split(".").Last()}";
                // Gọi service để upload file
                var fileUrl = await _staticFileServ.UploadImageAsync(employeeDTO.FileImage, fileName, key);
                // Update url image in database
                newEmployee.Image = fileUrl;
                await _emplRepo.SaveChangesAsync();
            }
            return _emplMapper.ToDTO(newEmployee);

        }

        public async Task<EmployeeDTO> Edit(EmployeeDTO employeeDTO)
        {
            var exist = await _emplRepo.ExistAsync(employeeDTO.Id);
            if (!exist)
            {
                throw new Exception("Employee does not exist.");
            }
            var employee = _emplMapper.ToModel(employeeDTO);
            _emplRepo.UpdateEmployeeIgnorePropeties(employee);
            await _emplRepo.SaveChangesAsync();
            return employeeDTO;
        }

        public async Task Delete(long employeeId)
        {
            var employee = await _emplRepo.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new Exception("Employee doesn't exist.");
            }
            await _emplRepo.DeleteAsync(employee);
        }

        public async Task<EmployeeDTO> Get(long employeeId)
        {
            Expression<Func<Employee, bool>> exppression = e => e.Id == employeeId;
            Expression<Func<Employee, object>>[] includes = [e => e.Department!];
            var employee = await _emplRepo.FindOneAsync(exppression, includes);
            if (employee == null)
            {
                throw new Exception("Employee doesn't exist.");
            }
            return _emplMapper.ToDTO(employee);
        }

        public async Task<ICollection<EmployeeDTO>> GetAll()
        {
            var employees = await _emplRepo.GetAllAsync();
            return _emplMapper.TolistDTO(employees);
        }

        public async Task<string[]> DeleteMany(long[] employeeIds)
        {
            string[] messages = new string[employeeIds.Length];
            for (var i = 0; i < employeeIds.Length; i++)
            {
                var employee = await _emplRepo.GetByIdAsync(employeeIds[i]);
                if (employee == null)
                {
                    messages[i] = $"Can't delete employee id = {employeeIds[i]}. Employee doesn't exist.";
                }
                else
                {
                    await _emplRepo.DeleteAsync(employee);
                    messages[i] = $"Delete employee id = {employeeIds[i]} successfully.";
                }
            }
            return messages;
        }

        public async Task<ICollection<EmployeeDTO>> SearchNameOrIdAsync(string keyword, long? departmentId)
        {
            Expression<Func<Employee, bool>> expression = e =>
                (e.Fullname.Contains(keyword) || e.Id.ToString().Equals(keyword)) && e.DepartmentId == departmentId;
            var employees = await _emplRepo.FindListAsync(expression);
            return _emplMapper.TolistDTO(employees);
        }

        public async Task<(ICollection<EmployeeDTO>, int totalPages, int totalRecords)> FilterAsync(EmployeeFilterDTO filter)
        {
            if (filter.Page < 1 || filter.PageSize < 1)
            {
                throw new ArgumentException("Page and PageSize must be >= 1.");
            }
            var (employees, totalPage, totalRecords) = await _emplRepo.FilterAsync(filter.NameOrId,
                filter.Address, filter.FromDoB, filter.ToDoB, filter.FromSalary, filter.ToSalary,
                filter.Position, filter.FromStartDate, filter.ToStartDate, filter.DepartmentId,
                filter.Status, filter.SortBy, filter.Page, filter.PageSize);
            return (_emplMapper.TolistDTO(employees), totalPage, totalRecords);
        }

        public async Task<bool> Lock(long id)
        {
            var empl = await _emplRepo.GetByIdAsync(id) ?? throw new Exception("Employee does not exist.");
            empl.Status = Status.Lock;
            await _emplRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnLock(long id)
        {
            var empl = await _emplRepo.GetByIdAsync(id) ?? throw new Exception("Employee does not exist.");
            empl.Status = Status.Active;
            await _emplRepo.SaveChangesAsync();
            return true;
        }

        public async Task UpdateImageAsync(long id, string fileUrl)
        {
            var employee = await _emplRepo.GetByIdAsync(id) ?? throw new Exception("Employee does not exist");
            employee.Image = fileUrl;
            await _emplRepo.SaveChangesAsync();
        }

        public async Task<bool> IsPersonOfDepartment(long? departmentId, long personId)
        {
            if (departmentId == null) throw new Exception("'departmentId' is requid");
            Expression<Func<Employee, bool>> expression = e => e.Id == personId && e.DepartmentId == departmentId;
            return await _emplRepo.FindOneAsync(expression) != null;
        }
    }
}
