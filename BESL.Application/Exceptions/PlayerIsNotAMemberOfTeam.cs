namespace BESL.Application.Exceptions
{
    public class PlayerIsNotAMemberOfTeam : BaseCustomException
    {
        public PlayerIsNotAMemberOfTeam()
            : base ("Player is not a member of the current team!")
        {
        }

        public PlayerIsNotAMemberOfTeam(string firstTeamName, string secondTeamName)
            :base($"Player is not a member of either {firstTeamName} or {secondTeamName}!")
        {
        }
    }
}
