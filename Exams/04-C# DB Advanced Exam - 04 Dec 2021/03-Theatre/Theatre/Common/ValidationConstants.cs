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
        public const int TheatreDirectorMinLength = 4;
        public const int TheatreDirectorMaxLength = 30;

        //Play
        public const int PlayTitleMinLength = 4;
        public const int PlayTitleMaxLength = 50;
        public const string PlayDurationMinValue = "1:0:0";
        public const float PlayRatingMinLength = 0f;
        public const float PlayRatingMaxLength = 10.0f;
        public const int PlayDescriptionMaxLength = 700;
        public const int PlayScreenwriterMinLength = 4;
        public const int PlayScreenwriterMaxLength = 30;

        //Cast
        public const int CastFullNameMinLength = 4;
        public const int CastFullNameMaxLength = 30;
        public const string CastPhoneNumberRegex = @"^((\+44)-[0-9]{2}-[0-9]{3}-[0-9]{4})$";

        //Ticket
        public const sbyte TicketRowNumberMinValue = 1;
        public const sbyte TicketRowNumberMaxValue = 10;

    }
}
