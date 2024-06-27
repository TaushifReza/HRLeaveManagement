using Application.Models.Identity;

namespace Application.Contract.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(string userId);
}