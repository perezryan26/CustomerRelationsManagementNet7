﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerRelationsManagement.Common.Constants;
using CustomerRelationsManagement.Application.Contracts;
using CustomerRelationsManagement.Data;
using CustomerRelationsManagement.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomerRelationsManagement.Application.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Employee> userManager;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;
        private readonly AutoMapper.IConfigurationProvider configurationProvider;
        private readonly IEmailSender emailSender;

        public LeaveAllocationRepository(ApplicationDbContext context
            , UserManager<Employee> userManager
            , ILeaveTypeRepository leaveTypeRepository
            , IMapper mapper
            , AutoMapper.IConfigurationProvider configurationProvider
            , IEmailSender emailSender) : base(context)
        {
            this.context = context;
            this.userManager = userManager;
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
            this.configurationProvider = configurationProvider;
            this.emailSender = emailSender;
        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
        {
            return await context.LeaveAllocations.AnyAsync(q => q.EmployeeId == employeeId
                                                           && q.LeaveTypeId == leaveTypeId
                                                           && q.Period == period);
        }

        public async Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string employeeId)
        {
            var allocations = await context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Where(q => q.EmployeeId == employeeId)
                .ProjectTo<LeaveAllocationViewModel>(configurationProvider)
                .ToListAsync();

            var employee = await context.Users
               .Include(q => q.Position)
               .FirstOrDefaultAsync(q => q.Id == employeeId);

            var employeeAllocationModel = mapper.Map<EmployeeAllocationViewModel>(employee);
            employeeAllocationModel.LeaveAllocations = allocations;
            
            return employeeAllocationModel;
        }

        public async Task<LeaveAllocationEditViewModel> GetEmployeeAllocation(int id)
        {
            var allocation = await context.LeaveAllocations
                .Include(q => q.LeaveType)
                .ProjectTo<LeaveAllocationEditViewModel>(configurationProvider) 
                .FirstOrDefaultAsync(q => q.Id == id);

            if(allocation == null)
            {
                return null;
            }

            var model = allocation;
            model.Employee = mapper.Map<EmployeeListViewModel>(await userManager.FindByIdAsync(allocation.EmployeeId));

            return model;
        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await leaveTypeRepository.GetAsync(leaveTypeId);
            var allocations = new List<LeaveAllocation>();
            var employeesWithUpdatedAllocations = new List<Employee>();

            
            foreach(var employee in employees)
            {
                if (await AllocationExists(employee.Id, leaveTypeId, period))
                    continue;

                allocations.Add(new LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveTypeId,
                    Period = period,
                    NumberOfDays = leaveType.DefaultDays
                });

                employeesWithUpdatedAllocations.Add(employee);
            }
            await AddRangeAsync(allocations);

            foreach(var employee in employeesWithUpdatedAllocations)
            {
                await emailSender.SendEmailAsync(employee.Email, $"Leave Allocation Posted for {period}", $"Your {leaveType.Name} " +
                    $"has been posted for the period of {period}. You have been given {leaveType.DefaultDays}.");
            }
        } 

        public async Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditViewModel model)
        {
            var leaveAllocation = await GetAsync(model.Id);
            if (leaveAllocation == null)
            {
                return false;
            }

            leaveAllocation.Period = model.Period;
            leaveAllocation.NumberOfDays = model.NumberOfDays;
            await UpdateAsync(leaveAllocation);

            var user = await userManager.FindByIdAsync(leaveAllocation.EmployeeId);

            await emailSender.SendEmailAsync(user.Email, $"Leave Allocation Updated for {leaveAllocation.Period}",
                "Please review your leave allocations.");

            return true;
        }

        public async Task<LeaveAllocation> GetEmployeeAllocation(string employeeId, int leaveTypeId)
        {
            return await context.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == employeeId && q.LeaveTypeId == leaveTypeId);
        }
    }
}
