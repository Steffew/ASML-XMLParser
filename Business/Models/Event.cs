namespace Business;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SourceId { get; set; }
    
    public List<Parameter> Parameters = new List<Parameter>();

    public Event(string name, string sourceId)
    {
        Name = name;
        SourceId = sourceId;
    }
}