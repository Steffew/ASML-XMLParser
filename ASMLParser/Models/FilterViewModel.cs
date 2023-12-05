namespace ASMLXMLParser.Models
{
	public class FilterViewModel
	{
		public List<string> MachineNames { get; set; }
		public List<string> MachineNameFilters { get; set; }
		public List<string> EventNames { get; set; }
		public List<string> MachineEventFilters { get; set; }
		public List<string> ParameterNames { get; set; }
		public List<string> MachineParameterFilters { get; set; }
		
		public FilterViewModel(List<string> machineNames, List<string> machineNameFilters, List<string> eventNames, List<string> machineEventFilters, List<string> parameterNames, List<string> machineParameterFilters)
		{
			MachineNames = machineNames;
			MachineNameFilters = machineNameFilters;
			EventNames = eventNames;
			MachineEventFilters = machineEventFilters;
			ParameterNames = parameterNames;
			MachineParameterFilters = machineParameterFilters;

		}
	}
}
