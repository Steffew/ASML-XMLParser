namespace ASMLXMLParser.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceId { get; set; }
        public List<ParameterViewModel> Parameters { get; set; }
        public EventViewModel(int id, string name, string sourceId, List<ParameterViewModel> parameters)
        {
            Id = id;
            Name = name;
            SourceId = sourceId;
            Parameters = parameters;
        }
    }
}
