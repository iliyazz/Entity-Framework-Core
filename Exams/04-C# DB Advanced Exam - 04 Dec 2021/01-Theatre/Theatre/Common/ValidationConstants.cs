namespace Theatre.Common
{
    using Theatre.Data.Models;

    public class ValidationConstants
    {
        //Theatre
        public const int TheatreNameMinLength = 4;
        public const int TheatreNameMaxLength = 30;
        public const sbyte TheatreNumberOfHallsMinValue = 1;
        public const sbyte TheatreNumberOfHallsMaxValue = 10;
        public const int TheatreDirectorMinValue = 4;
        public const int TheatreDirectorMaxValue = 30;

        //Play
        public const int PlayTitleMinLength = 4;
        public const int PlayTitleMaxLength = 50;
        public const string PlayDurationMinLength = "1:0:0";
        public const float PlayRatingMinLength = 0f;
        public const float PlayRatingMaxLength = 10.0f;
        public const int PlayDescriptionMaxLength = 700;
        public const int PlayScreenwriterMinLength = 4;
        public const int PlayScreenwriterMaxLength = 30;

        //Cast
        public const int CastFullNameMinLength = 4;
        public const int CastFullNameMaxLength = 30;
        public const int CastPhoneNumberLength = 15;

        //Ticket
        public const decimal TicketPriceMinValue = 1.0m;
        public const decimal TicketPriceMaxValue = 100.0m;
        public const sbyte TicketRowNumberMinValue = 1;
        public const sbyte TicketRowNumberMaxValue = 10;

    }
}
