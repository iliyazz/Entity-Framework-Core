namespace Artillery.Common
{
    public class ValidationConstants
    {
        //Country
        public const int CountryCountryNameMinLength = 4;
        public const int CountryCountryNameMaxLength = 60;
        public const int CountryArmySizeMinValue = 50000;
        public const int CountryArmySizeMaxValue = 100000000;

        //Manufacturer
        public const int ManufacturerManufacturerNameMinLength = 4;
        public const int ManufacturerManufacturerNameMaxLength = 40;
        public const int ManufacturerFoundedNameMinLength = 10;
        public const int ManufacturerFoundedNameMaxLength = 100;

        //Shell
        public const double ShellShellWeightMinValue = 2.0;
        public const double ShellShellWeightMaxValue = 1680.0;
        public const int ShellCaliberMinLength = 4;
        public const int ShellCaliberMaxLength = 30;

        //Gun
        public const int GunGunWeightMinValue = 100;
        public const int GunGunWeightMaxValue = 1350000;
        public const double GunBarrelLengthMinValue = 2.0;
        public const double GunBarrelLengthMaxValue = 35.0;
        public const int GunRangeMinValue = 1;
        public const int GunRangeMaxValue = 100000;


    }
}
