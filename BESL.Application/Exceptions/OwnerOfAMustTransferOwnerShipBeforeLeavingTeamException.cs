namespace BESL.Application.Exceptions
{
    public class OwnerOfAMustTransferOwnerShipBeforeLeavingTeamException : BaseCustomException
    {
        public OwnerOfAMustTransferOwnerShipBeforeLeavingTeamException()
            : base("You must transfer team ownership before leaving yourself!")
        {
        }
    }
}
