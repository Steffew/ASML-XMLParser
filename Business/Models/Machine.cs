namespace Business;

public class Machine
{
    public int Id;
    public string Namespace;
    public List<Event> Events = new List<Event>();
}