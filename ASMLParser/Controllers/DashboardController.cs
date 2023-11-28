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

        public IActionResult Index(List<string> filters)
        {


            List<MachineViewModel> machineViewModels = new List<MachineViewModel>();
            MachineService machineService = new MachineService();
            List<string> machineNames = new List<string>();
            
            foreach (var machine in machineService.GetAll())
            {
                if (!machineNames.Contains(machine.Name))
                {
                    machineNames.Add(machine.Name);
                }
            }
            
            var machines = machineService.GetAll();
            
            if (filters.Any())
            {
                foreach (var filter in filters)
                {
                    machines = machines.Where(m => filters.Any(filter => m.Name == filter)).ToList();

                }
                
            }

            foreach (Machine machine in machines)
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

                    EventViewModel eventView = new(machineEvent.Id, machineEvent.Name, machineEvent.SourceId,
                        parameters);
                    events.Add(eventView);
                }

                MachineViewModel machineModel = new(machine.Id, machine.Name, events);
                machineViewModels.Add(machineModel);
            }

            int totalMachines = machineService.GetTotalAmountOfMachines();
            int totalEvents = machineService.GetTotalAmountOfEvents();
            int totalParameters = machineService.GetTotalAmountOfParameters();
            DashboardViewModel dashboardView =
                new DashboardViewModel(totalMachines, machineNames, filters, totalEvents, totalParameters, machineViewModels);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Filter(IFormCollection formCollection)
        {
            List<string> checkedFilters = new List<string>();

            foreach (var key in formCollection.Keys)
            {
                if (key != "__RequestVerificationToken")
                {
                    checkedFilters.Add(key);
                }
            }

            return RedirectToAction(nameof(Index), new { filters = checkedFilters });
        }
    }
}