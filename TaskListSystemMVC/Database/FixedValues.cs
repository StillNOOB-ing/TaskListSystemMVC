namespace TaskListSystemMVC.Database
{
    public class FixedStatus
    {
        public const int NEW_UID = 1;
        public const string NEW_NAME = "New";
        public const int WIP_UID = 2;
        public const string WIP_NAME = "Work In Progress";
        public const int HOLD_UID = 3;
        public const string HOLD_NAME = "Hold";
        public const int COMPLETED_UID = 4;
        public const string COMPLETED_NAME = "Completed";
    }

    public class FixedUserLevel
    {
        public const int SUPERADMIN_UID = 1;
        public const string SUPERADMIN_NAME = "Super Admin";
        public const int ADMIN_UID = 2;
        public const string ADMIN_NAME = "Admin";
        public const int CUSTOMER_UID = 3;
        public const string CUSTOMER_NAME = "Customer";
        public const int GUEST_UID = 4;
        public const string GUEST_NAME = "Guest";
    }
}
