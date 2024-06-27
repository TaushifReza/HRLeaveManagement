namespace Application.Models.Identity
{
    public class AuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Employee
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
