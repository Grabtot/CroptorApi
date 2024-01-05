namespace Croptor.Domain.Common.Exceptions
{
    public class UserNotAuthenticatedException : Exception
    {
        public override string Message => "Not authenticated user cant do this";
    }
}
