namespace ASMLXMLParser.Models
{
    public class ParameterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceId { get; set; }
        public ParameterViewModel(int id, string name, string sourceId)
        {
            Id = id;
            Name = name;
            SourceId = sourceId;
        }
        public ParameterViewModel() { }
    }
}
