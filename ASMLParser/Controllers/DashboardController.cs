using ASMLXMLParser.Models;
using Business;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;

namespace ASMLXMLParser.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(List<string> filtersName, List<string> filtersEvent, List<string> filtersParameter)
        {
            List<MachineViewModel> machineViewModels = new List<MachineViewModel>();
            MachineService machineService = new MachineService();

            var machines = machineService.GetAll();

            if (filtersName.Any())
            {
                machines = machines.Where(m => filtersName.Any(filter => m.Name == filter))
                    .ToList();
            }

            if (filtersEvent.Any())
            {
                machines = machines.Where(m => m.Events.Any(evt => filtersEvent.Any(filter => evt.Name == filter)))
                    .ToList();
            }

            if (filtersParameter.Any())
            {
                machines = machines.Where(m =>
                        m.Events.Any(evt => evt.Parameters.Any(p => filtersParameter.Any(filter => p.Name == filter))))
                    .ToList();
            }

            foreach (Machine machine in machines)
            {
                List<EventViewModel> events = new();
                foreach (Event machineEvent in machine.Events)
                {

                    List<ParameterViewModel> parameters = new();
                    foreach (Parameter parameter in machineEvent.Parameters)
                    {
                        if (!filtersParameter.Any() || filtersParameter.Contains(parameter.Name))
                        {
                            
                            ParameterViewModel machineParameter = new(parameter.Id, parameter.Name, parameter.SourceId);
                            parameters.Add(machineParameter);
                        }

                    }

                    if (!filtersEvent.Any() || filtersEvent.Contains(machineEvent.Name))
                    {
                        EventViewModel eventView = new(machineEvent.Id, machineEvent.Name, machineEvent.SourceId,
                            parameters);
                        events.Add(eventView);
                    }

                }

                MachineViewModel machineModel = new(machine.Id, machine.Name, events);
                machineViewModels.Add(machineModel);
            }

            List<String> uniqueMachineNames = new List<string>();
            List<String> uniqueEventNames = new List<string>();
            List<String> uniqueParameterNames = new List<string>();

            foreach (var machineViewModel in machineViewModels)
            {
                foreach (var eventModel in machineViewModel.Events)
                {
                    foreach (var parameter in eventModel.Parameters)
                    {
                        uniqueMachineNames.Add(machineViewModel.Name);
                        uniqueEventNames.Add(eventModel.Name);
                        uniqueParameterNames.Add(parameter.Name);
                    }
                }
            }
            

            FilterViewModel filterViewModel =
                new FilterViewModel(uniqueMachineNames.Distinct().ToList(), filtersName, uniqueEventNames.Distinct().ToList(), filtersEvent, uniqueParameterNames.Distinct().ToList(),
                    filtersParameter);

            DashboardViewModel dashboardView =
                new DashboardViewModel(uniqueMachineNames.Distinct().Count(), uniqueEventNames.Distinct().Count(), uniqueParameterNames.Distinct().Count(), machineViewModels, filterViewModel);
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
            List<string> checkedNameFilters = new List<string>();
            List<string> checkedEventFilters = new List<string>();
            List<string> checkedParameterFilters = new List<string>();

            foreach (var key in formCollection.Keys)
            {
                if (key != "__RequestVerificationToken")
                {
                    if (key.StartsWith("n_"))
                    {
                        checkedNameFilters.Add(key.Substring(2));
                    }

                    if (key.StartsWith("e_"))
                    {
                        checkedEventFilters.Add(key.Substring(2));
                    }

                    if (key.StartsWith("p_"))
                    {
                        checkedParameterFilters.Add(key.Substring(2));
                    }
                }
            }

            return RedirectToAction(nameof(Index),
                new
                {
                    filtersName = checkedNameFilters, filtersEvent = checkedEventFilters,
                    filtersParameter = checkedParameterFilters
                });
        }
    }
}