using DAL.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DAL
{
    public class Upload
    {
        public void UploadMachine(MachineDTO machine)
        {
            ServerConnection dal = new ServerConnection();
            SqlConnection sqlConnection = dal.GetConnection();
            MachineRepository machineRepository = new();
            MachineDTO machineCheck = machineRepository.LoadMachineByName(machine.MachineName);
            int machineId;
            if (machineCheck.MachineName == null)
            {
                SqlCommand command = new SqlCommand("INSERT INTO dbo.Machine(MachineName) VALUES('" + machine.MachineName + "');", sqlConnection);
                dal.UploadData(command);
                machineCheck = machineRepository.LoadMachineByName(machine.MachineName);
                machineId = machineCheck.MachineID;
            }
            else
            {
                machineId = machineCheck.MachineID;
            }
            List<string> eventNames = new();
            List<string> parameterNames = new();
            List<EventDTO> machineEvents = machineRepository.LoadEventsByMachineID(machineId);
            foreach (EventDTO machineEvent in machineEvents)
            {
                eventNames.Add(machineEvent.EventName);
            }
            foreach(EventDTO eventDTO in machine.Events)
            {
                if (!eventNames.Contains(eventDTO.EventName))
                {
                    SqlCommand eventCommand = new SqlCommand("INSERT INTO dbo.Events(EventName, EventSourceID) VALUES(@EventName, @EventSourceID)", sqlConnection);
                    SqlCommand machineEventCommand = new("INSERT INTO dbo.Machine_Event(MachineID, EventID) VALUES(@MachineID, @EventID)", sqlConnection);
                    eventCommand.Parameters.AddWithValue("@EventName", eventDTO.EventName);
                    eventCommand.Parameters.AddWithValue("@EventSourceID", eventDTO.EventSourceID);
                    dal.UploadData(eventCommand);
                    int eventID = machineRepository.LatestUploadEventID();
                    machineEventCommand.Parameters.AddWithValue("@MachineID", machineId);
                    machineEventCommand.Parameters.AddWithValue("@EventID", eventID);
                    dal.UploadData(machineEventCommand);
                }
                foreach(ParameterDTO paramterDTO in eventDTO.Parameters)
                {
                    SqlCommand parameterCommand = new("INSERT INTO dbo.Parameters(ParameterName, ParameterSource) VALUES(@ParameterName, @ParameterSource)", sqlConnection);

                }
            }   
        }
    }
}
