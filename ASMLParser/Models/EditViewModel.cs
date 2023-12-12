namespace ASMLXMLParser.Models
{
    public class EditViewModel
    {
        public UserViewModel User { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        public EditViewModel(UserViewModel user, List<RoleViewModel> roles)
        {
            User = user;
            Roles = roles;
        }
    }
}
