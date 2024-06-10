namespace Application.Services
{
    /// <summary>
    /// Provide info for the signed user
    /// </summary>
    public interface IUserAccessor
    {
        /// <returns>Get signed user id or null if there is no signed user</returns>
        string GetUserId();

        /// <summary>
        /// Sets the user id and return true if it exists
        /// </summary>
        bool TryGetUserId(out string userId);
    }
}