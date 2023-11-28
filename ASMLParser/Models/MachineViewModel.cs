namespace ASMLXMLParser.Models
{
    public class MachineViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EventViewModel> Events { get; set; }
        public MachineViewModel(int id, string name, List<EventViewModel> events)
        {
            Id = id;
            Name = name;
            Events = events;
        }
    }
}
