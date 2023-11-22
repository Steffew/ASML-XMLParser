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
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;
using Azure.Identity;

namespace DAL
{
    public class Upload
    {
        public void UploadMachine(MachineDTO machine)
        {
            ServerConnection dal = new ServerConnection();
            MachineRepository machineRepository = new();
            MachineDTO machineCheck = machineRepository.LoadMachineByName(machine.MachineName);
            if (machineCheck.MachineName != null)
            {
                machineRepository.RemoveMachineById(machineCheck.MachineID);
            }
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Machine](MachineName) VALUES('" + machine.MachineName + "');");
            dal.UploadData(command);
            machineCheck = machineRepository.LoadMachineByName(machine.MachineName);
            int machineId = machineCheck.MachineID;
            List<string> eventNames = new();
            List<string> parameterNames = new();
            List<EventDTO> machineEvents = machineRepository.LoadEventsByMachineID(machineId);
            int eventID = 0;
            foreach (EventDTO eventDTO in machine.Events)
            {
                List<EventDTO> tempevents = new(machineEvents.FindAll(dto => dto.EventID == eventDTO.EventID));
                if (tempevents.IsNullOrEmpty() || !tempevents.Exists(dto => dto.EventSourceID == eventDTO.EventSourceID))
                {
                    SqlCommand eventCommand = new SqlCommand("INSERT INTO [dbo].[Event](EventName, EventSource) VALUES(@EventName, @EventSourceID)");
                    SqlCommand machineEventCommand = new("INSERT INTO [dbo].[Machine_Event](MachineID, EventID) VALUES(@MachineID, @EventID)");
                    eventCommand.Parameters.AddWithValue("@EventName", eventDTO.EventName);
                    eventCommand.Parameters.AddWithValue("@EventSourceID", eventDTO.EventSourceID);
                    dal.UploadData(eventCommand);
                    eventID = machineRepository.LatestUploadEventID();
                    machineEventCommand.Parameters.AddWithValue("@MachineID", machineId);
                    machineEventCommand.Parameters.AddWithValue("@EventID", eventID);
                    dal.UploadData(machineEventCommand);
                }
                else
                {
                    EventDTO tempevent = tempevents.Find(dto => dto.EventSourceID == eventDTO.EventSourceID);
                    eventID = tempevent.EventID;
                }
                List<ParameterDTO> eventParameters = machineRepository.LoadParametersByEventID(eventID);
                foreach(ParameterDTO paramterDTO in eventDTO.Parameters)
                {
                    List<ParameterDTO> tempParameters = eventParameters.FindAll(dto => dto.ParameterName == paramterDTO.ParameterName);
                    if (tempParameters.IsNullOrEmpty() || tempParameters.Exists(dto => dto.ParameterSourceID == paramterDTO.ParameterSourceID))
                    {
                        SqlCommand parameterCommand = new("INSERT INTO [dbo].[Parameter](ParameterName, ParameterSource) VALUES(@ParameterName, @ParameterSource)");
                        SqlCommand eventParameterCommand = new("INSERT INTO [dbo].[Event_Parameter](EventID, ParameterID) VALUES(@EventID, @ParameterID)");
                        parameterCommand.Parameters.AddWithValue("@ParameterName", paramterDTO.ParameterName);
                        parameterCommand.Parameters.AddWithValue("@ParameterSource", paramterDTO.ParameterSourceID);
                        dal.UploadData(parameterCommand);
                        int parameterid = machineRepository.LatestUploadParameterID();
                        eventParameterCommand.Parameters.AddWithValue("@EventID", eventID);
                        eventParameterCommand.Parameters.AddWithValue("@ParameterID", parameterid);
                        dal.UploadData(eventParameterCommand);
                    }
                }
            }   
        }

        public void CreateUser(UserDTO userDTO)
        {
            /* nSqlCommand userCommand = new("INSERT INTO [dbo].[User](UserName, Password, RoleID) VALUES(@Username, @Password, RoleID)");
            userCommand.Parameters.AddWithValue("@Username", userDTO.Name);
            userCommand.Parameters.AddWithValue();
            userCommand.Parameters.AddWithValue();*/
        }
    }
}
