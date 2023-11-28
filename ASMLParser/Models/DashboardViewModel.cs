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
		public FilterViewModel FilterViewModel { get; set; }
		public DashboardViewModel(int totalMachines, int totalEvents, int totalParameters, List<MachineViewModel> machines, FilterViewModel filterViewModel)
		{
			TotalMachines = totalMachines;
			TotalEvents = totalEvents;
			TotalParameters = totalParameters;
			Machines = machines;
			FilterViewModel = filterViewModel;
		}
	}
}
