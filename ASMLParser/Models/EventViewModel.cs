namespace ASMLXMLParser.Models
{
    public class EventViewModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string SourceId { get; set; }
        public required List<ParameterViewModel> Parameters { get; set; }
    }
}
