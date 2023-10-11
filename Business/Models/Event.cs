namespace Business;

public class Event
{
    public string Id;
    public string SourceId;
    public List<Parameter> Parameters = new List<Parameter>();

    public Event(string id)
    {
        Id = id;
    }
}