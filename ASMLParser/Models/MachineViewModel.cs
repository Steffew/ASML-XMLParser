namespace ASMLXMLParser.Models
{
    public class MachineViewModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required List<EventViewModel> Events { get; set; }
    }
}
