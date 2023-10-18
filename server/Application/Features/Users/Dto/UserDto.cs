namespace Application.Users.Dto
{
    public class UserDto 
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Id { get; set; }
    }

    public class AuthUserDto : UserDto
    {
        public string Token { get; set; }
    }
}