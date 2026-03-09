namespace CinemaWebAppOriginal.Areas.Admin.Models.UserManagement
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();

    }
}
