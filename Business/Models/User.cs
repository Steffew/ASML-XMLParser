namespace Business;

public class User
{
    public int Id;
    public string Name;
    public string Password;
    public Role Role;

    public User(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public User(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

