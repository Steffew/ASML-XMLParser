namespace Business;

public class User
{
    public int Id;
    public string Name;
    public Role Role;

    public User(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public void SetRole(Role role)
    {
        Role = role;
    }
    public void SetName(string name)
    {
        Name = name;
    }
}

