namespace ASMLXMLParser.Models
{
	public class FilterViewModel
	{
		public List<string> MachineNames { get; set; }
		public List<string> MachineNameFilters { get; set; }
		public List<string> EventNames { get; set; }
		public List<string> ParameterNames { get; set; }
		
		public FilterViewModel(List<string> machineNames, List<string> machineNameFilters, List<string> eventNames, List<string> parameterNames)
		{
			MachineNames = machineNames;
			MachineNameFilters = machineNameFilters;
			EventNames = eventNames;
			ParameterNames = parameterNames;

		}
	}
}
