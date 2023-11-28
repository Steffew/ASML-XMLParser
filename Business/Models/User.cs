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
}

