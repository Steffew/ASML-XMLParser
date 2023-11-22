namespace ASMLXMLParser.Models
{
    public class ProfileViewModel
    {
        public UserViewModel CurrentUser { get; set; }
        public List<UserViewModel> Users { get; set; }
        public ProfileViewModel(UserViewModel currentUser, List<UserViewModel> users)
        {
            CurrentUser = currentUser;
            Users = users;
        }
    }
}
