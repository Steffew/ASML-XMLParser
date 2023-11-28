namespace ASMLXMLParser.Models
{
    public class UserViewModel
    {
        public int Id;
        public string Name;
        public RoleViewModel Role;

        public UserViewModel(int id, string name, RoleViewModel role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
    }
}
