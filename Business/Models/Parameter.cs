namespace Business;

public class Parameter
{
    public int Id;
    public string Name;
    public string SourceId;

    public Parameter(string name, string sourceId)
    {
        Name = name;
        SourceId = sourceId;
    }
}

