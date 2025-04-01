namespace TaskListSystemMVC.Helper
{
    public interface IAccountHelper
    {
        public string GetName();
        public int GetUserLevelID();
        public string HashPassword(string inputPassword);
        public bool VerifyPassword(string inputPassword, string storedHash);
    }
}
