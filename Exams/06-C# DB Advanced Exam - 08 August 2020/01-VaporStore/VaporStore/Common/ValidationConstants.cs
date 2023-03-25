namespace VaporStore.Common
{

    public class ValidationConstants
    {
        //User
        public const int UserUsernameMinLength = 3;
        public const int UserUsernameMaxLength = 20;
        public const string UserFullNameValidationRegex = @"^([A-Z]{1}[a-z]+)\s([A-Z]{1}[a-z]+)$";
        public const int UserAgeMinValue = 3;
        public const int UserAgeMaxValue = 103;

        //Card
        public const string CardNumberValidationRegex = @"^([0-9]{4}\s[0-9]{4}\s[0-9]{4}\s[0-9]{4})$";
        public const string CardCvcValidationRegex = @"^([0-9]{3})$";

        //Purchase
        public const string PurchaseProductKeyValidationRegex = @"^([A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4})$";
    }
}
