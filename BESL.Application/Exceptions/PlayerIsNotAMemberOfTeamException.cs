namespace BESL.Application.Exceptions
{
    public class PlayerIsNotAMemberOfTeamException : BaseCustomException
    {
        public PlayerIsNotAMemberOfTeamException()
            : base("Player is not a member of the current team!")
        {
        }

        public PlayerIsNotAMemberOfTeamException(string firstTeamName, string secondTeamName)
            : base($"Player is not a member of either {firstTeamName} or {secondTeamName}!")
        {
        }
    }
}
