namespace BESL.Application.Tournaments.Commands.Validation
{
    public static class Constants
    {
        public const int NAME_MIN_LENGTH = 10, NAME_MAX_LENGTH = 50;
        public const int DESC_MIN_LENGTH = 50, DESC_MAX_LENGTH = 500;
        public const int END_DATE_MIN_MONTH_LENGTH = 1;
        public const int END_DATE_MAX_DAYS_DIFF = -7;

        public const string START_DATE_MSG = "Start date cannot be in the past!";
        public const string START_DATE_MUST_BE_MONDAY_MSG = "Start date must be on Monday!";

        public const string END_DATE_MSG = "End date must be at least {0} month after the start date!";
        public const string END_DATE_MUST_BE_SUNDAY_MSG = "End date must be on Sunday!";

        public const string NAME_LENGTH_MSG = "Tournament name length must be between {0} and {1} characters long!";
        public const string DESC_LENGTH_MSG = "Tournament description length must be between {0} and {1} characters long!";
    }
}
