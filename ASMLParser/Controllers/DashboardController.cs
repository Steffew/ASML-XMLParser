using ASMLXMLParser.Models;
using Business;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASMLXMLParser.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<MachineViewModel> machineViewModels = new List<MachineViewModel>();
            MachineService machineService = new MachineService();
            machineService.GetAll();

            foreach (Machine machine in machineService.GetAll())
            {
                List<EventViewModel> events = new();
                foreach (Event machineEvent in machine.Events)
                {
                    List<ParameterViewModel> parameters = new();
                    foreach (Parameter parameter in machineEvent.Parameters)
                    {
                        ParameterViewModel machineParameter = new(parameter.Id, parameter.Name, parameter.SourceId);
                        parameters.Add(machineParameter);
                    }
                    EventViewModel eventView = new(machineEvent.Id, machineEvent.Name, machineEvent.SourceId, parameters);
                    events.Add(eventView);
                }
                MachineViewModel machineModel = new(machine.Id, machine.Name, events);
                machineViewModels.Add(machineModel);
            }
            

            int totalMachines = machineService.GetTotalMachines(machineService.GetAll());
            int totalEvents = machineService.GetTotalEvents();
            int totalParameters = machineService.GetTotalParameters();
            DashboardViewModel dashboardView = new DashboardViewModel(totalMachines, totalEvents, totalParameters, machineViewModels);
            return View(dashboardView);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}