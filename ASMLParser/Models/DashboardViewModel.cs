namespace ASMLXMLParser.Models
{
	public class DashboardViewModel
	{
		public int TotalMachines { get; set; }
		public List<string> MachineNames { get; set; }
		public List<string> Filters { get; set; }
		public int TotalEvents { get; set; }
		public int TotalParameters { get; set; }
		public List<MachineViewModel> Machines { get; set; }
		public DashboardViewModel(int totalMachines, List<string> machineNames, List<string> filters, int totalEvents, int totalParameters, List<MachineViewModel> machines)
		{
			TotalMachines = totalMachines;
			MachineNames = machineNames;
			Filters = filters;
			TotalEvents = totalEvents;
			TotalParameters = totalParameters;
			Machines = machines;
		}
	}
}
