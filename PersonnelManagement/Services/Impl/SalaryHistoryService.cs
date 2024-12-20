﻿using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;
using System.Linq.Expressions;

namespace PersonnelManagement.Services.Impl
{
    public class SalaryHistoryService : ISalaryHistoryService
    {
        private readonly IGenericRepository<SalaryHistory> _genericSHRepo;
        private readonly SalaryHistoryMapper _sHMapper;
        private readonly ISalaryHistoryRepository _sHRepo;

        public SalaryHistoryService(IGenericRepository<SalaryHistory> repository, ISalaryHistoryRepository salaryHistoryRepository)
        {
            _genericSHRepo = repository;
            _sHRepo = salaryHistoryRepository;
            _sHMapper = new SalaryHistoryMapper();
        }

        public async Task<SalaryHistoryDTO> Add(SalaryHistoryDTO salaryHistoryDTO)
        {
            var newSalaryHistory = _sHMapper.ToEntity(salaryHistoryDTO);
            await _genericSHRepo.AddAsync(newSalaryHistory);
            return _sHMapper.ToDTO(newSalaryHistory);

        }

        public async Task<SalaryHistoryDTO> Edit(SalaryHistoryDTO salaryHistoryDTO)
        {
            var exist = await _genericSHRepo.ExistAsync(salaryHistoryDTO.Id);
            if (!exist)
            {
                throw new Exception("Salary History does not exist.");
            }
            var salaryHistory = _sHMapper.ToEntity(salaryHistoryDTO);
            await _genericSHRepo.UpdateAsync(salaryHistory);
            return salaryHistoryDTO;
        }

        public async Task Delete(long salaryHistoryId)
        {
            var salaryHistory = await _genericSHRepo.GetByIdAsync(salaryHistoryId)
                ?? throw new Exception("Salary History doesn't exist.");
            await _genericSHRepo.DeleteAsync(salaryHistory);
        }

        public async Task<SalaryHistoryDTO> Get(long salaryHistoryId)
        {
            Expression<Func<SalaryHistory, object>>[] includes = [s => s.Employee];
            Expression<Func<SalaryHistory, bool>> expression = s => s.Id == salaryHistoryId;
            var salaryHistory = await _genericSHRepo.FindOneAsync(expression, includes);
            return salaryHistory == null
                ? throw new Exception("Salary History doesn't exist.")
                : _sHMapper.ToDTO(salaryHistory);
        }

        public async Task<ICollection<SalaryHistoryDTO>> GetAll()
        {
            Expression<Func<SalaryHistory, object>>[] includes = [s => s.Employee];
            var salaryHistories = await _genericSHRepo.FindListAsync(null, includes);
            return _sHMapper.TolistDTO(salaryHistories);
        }

        public async Task<string[]> DeleteMany(long[] salaryHistoryIds)
        {
            string[] messages = new string[salaryHistoryIds.Length];
            for (var i = 0; i < salaryHistoryIds.Length; i++)
            {
                var salaryHistory = await _genericSHRepo.GetByIdAsync(salaryHistoryIds[i]);
                if (salaryHistory == null)
                {
                    messages[i] = $"Can't delete salary history id = {salaryHistoryIds[i]}. Salary History doesn't exist.";
                }
                else
                {
                    await _genericSHRepo.DeleteAsync(salaryHistory);
                    messages[i] = $"Delete salary history id = {salaryHistoryIds[i]} successfully.";
                }
            }
            return messages;
        }

        public async Task<(ICollection<SalaryHistoryDTO>, int, int)> FilterAsync(SalaryHistoryFilterDTO filter)
        {
            if (filter.Page < 1 || filter.PageSize < 1)
            {
                throw new ArgumentException("Page and PageSize must be >= 1.");
            }
            var (salaryHistories, totalPage, totalRecords) = await _sHRepo.FilterAsync(filter.SortByDate,
                filter.EmployeeId, filter.Page, filter.PageSize);
            return (_sHMapper.TolistDTO(salaryHistories), totalPage, totalRecords);
        }

        public async Task<SalaryHistoryDTO> GetByEmployee(long salaryHistoryId, long emplyeeId)
        {
            Expression<Func<SalaryHistory, object>>[] includes = [s => s.Employee];
            Expression<Func<SalaryHistory, bool>> expression = s => s.Id == salaryHistoryId && s.EmployeeId == emplyeeId;
            var salaryHistory = await _genericSHRepo.FindOneAsync(expression, includes);
            return salaryHistory == null
                ? throw new Exception("Salary History doesn't exist.")
                : _sHMapper.ToDTO(salaryHistory);
        }

        public async Task<(ICollection<SalaryHistoryDTO>, int, int)> GetPagesByEmployeeAsync(
            int pageNumber, int pageSize, long employeeId)
        {
            var (salaryHistories, totalPage, totalRecords) = await _sHRepo.GetPagedListByEmployeeAsync(pageNumber, pageSize, employeeId);
            return (_sHMapper.TolistDTO(salaryHistories), totalPage, totalRecords);
        }
    }
}
