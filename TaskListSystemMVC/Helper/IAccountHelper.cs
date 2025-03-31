namespace TaskListSystemMVC.Helper
{
    public interface IAccountHelper
    {
        public string GetName();
        public string HashPassword(string inputPassword);
        public bool VerifyPassword(string inputPassword, string storedHash);
    }
}
