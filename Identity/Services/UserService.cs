using Application.Contract.Identity;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(q => new Employee
                {
                    Id = q.Id,
                    FirstName = q.FirstName,
                    LastName = q.LastName,
                    Email = q.Email
                }).ToList();
        }

        public async Task<Employee> GetEmployee(string userId)
        {
            var employees = await _userManager.FindByIdAsync(userId: userId);
            return new Employee
            {
                Email = employees.Email,
                Id = employees.Id,
                FirstName = employees.FirstName,
                LastName = employees.LastName,
            };

        }
    }
}
