namespace TeisterMask.Common
{
    public class ValidationConstants
    {
        //Employee
        public const string EmployeeUsernameRegex = @"^([A-Za-z0-9]){3,40}$";
        public const string EmployeePhoneRegex = @"^([0-9]{3}-[0-9]{3}-[0-9]{4})$";

        //Project
        public const int ProjectNameMinLength = 2;
        public const int ProjectNameMaxLength = 40;

        //Task
        public const int TaskNameMinLength = 2;
        public const int TaskNameMaxLength = 40;







    }
}
