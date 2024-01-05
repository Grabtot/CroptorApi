namespace Croptor.Application.Common.Interfaces
{
    public interface IUserProvider
    {
        Guid? UserId { get; }
        string? UserName { get; }
    }
}
