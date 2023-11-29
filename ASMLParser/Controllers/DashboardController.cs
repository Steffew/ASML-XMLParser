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
            List<string> machineNames = new List<string>();
            List<string> eventNames = new List<string>();
            List<string> parameterNames = new List<string>();

            foreach (var machine in machineService.GetAll())
            {
                if (!machineNames.Contains(machine.Name))
                {
                    machineNames.Add(machine.Name);
                }

                foreach (var _event in machine.Events)
                {
                    if (!eventNames.Contains(_event.Name))
                    {
                        eventNames.Add(_event.Name);
                    }

                    foreach (var parameter in _event.Parameters)
                    {
                        if (!parameterNames.Contains(parameter.Name))
                        {
                            parameterNames.Add(parameter.Name);
                        }
                    }
                }
            }


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

            FilterViewModel filterViewModel =
                new FilterViewModel(machineNames, filtersName, eventNames, filtersEvent, parameterNames,
                    filtersParameter);

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

            // int totalMachines = machineService.GetTotalAmountOfMachines(); //TODO: methode met machines toevoegen.
            // int totalEvents = machineService.GetTotalAmountOfEvents();
            // int totalParameters = machineService.GetTotalAmountOfParameters();

            List<string> uniqueMachines = new List<string>();
            List<string> uniqueParameters = new List<string>();
            List<string> uniqueEvents = new List<string>();

            foreach (var machine in machines)
            {
                if (!uniqueMachines.Contains(machine.Name))
                {
                    uniqueMachines.Add(machine.Name);
                }
                foreach (var _event in machine.Events)
                {
                    if (!uniqueEvents.Contains(_event.Name))
                    {
                        uniqueEvents.Add(_event.Name);
                    }
                    foreach (var parameter in _event.Parameters)
                    {
                        if (!uniqueParameters.Contains(parameter.Name))
                        {
                            uniqueParameters.Add(parameter.Name);
                        }
                    }
                }
            }

            DashboardViewModel dashboardView =
                new DashboardViewModel(uniqueMachines.Count, uniqueEvents.Count, uniqueParameters.Count, machineViewModels, filterViewModel);
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