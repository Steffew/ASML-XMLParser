namespace Business;

public class Machine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Event> Events = new List<Event>();

    public Machine(string name)
    {
        Name = name;
    }
}