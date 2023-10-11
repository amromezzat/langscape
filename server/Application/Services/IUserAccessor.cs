namespace Application.Services
{
    /// <summary>
    /// Provide info for the signed user
    /// </summary>
    public interface IUserAccessor
    {
        /// <returns>Get signed user id</returns>
        string GetUserId();
    }
}