namespace SoftJail.Common
{


    public class ValidationConstatnts
    {
        //Prisoner
        public const int PrisonerFullnameMinLength = 3;
        public const int PrisonerFullnameMaxLength = 20;
        public const string PrisonerNickNameRegex = @"^(The\s)([A-Z][a-z]*)$";
        public const int PrisonerAgeMinValue = 18;
        public const int PrisonerAgeMaxValue = 65;



        //Officer
        public const int OfficerFullnameMinLength = 3;
        public const int OfficerFullnameMaxLength = 30;

        //Department
        public const int DepartmentNameMinLength = 3;
        public const int DepartmentNameMaxLength = 25;

        //Cell
        public const int CellNumberMinLength = 1;
        public const int CellNumberMaxLength = 1000;

        //Mail
        public const string MailAddressRegex = @"^([A-Za-z\s0-9]+?)(\sstr\.)$";

        //Common
        public const string NonNegativeDecimalMinValue = "0";
        public const string NonNegativeDecimalMaxValue = "79228162514264337593543950335";
    }
}
