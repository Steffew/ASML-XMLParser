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

        ServerConnection dal = new ServerConnection();

        public void UploadMachine(MachineDTO machine)
        {
            MachineRepository machineRepository = new();
            MachineDTO machineCheck = machineRepository.LoadMachineByName(machine.MachineName);
            if (machineCheck.MachineName != null)
            {
                machineRepository.RemoveMachineById(machineCheck.MachineID);
            }
            SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Machine](MachineName) VALUES('" + machine.MachineName + "');");
            dal.UploadData(command);
            
            int machineId = machineRepository.LoadMachineByName(machine.MachineName).MachineID;
            int eventID;
            foreach (EventDTO eventDTO in machine.Events)
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
                foreach(ParameterDTO paramterDTO in eventDTO.Parameters)
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

        public void CreateUser(UserDTO userDTO)
        {
            SqlCommand userCommand = new("INSERT INTO [dbo].[User](UserName, Password, RoleID) VALUES(@Username, @Password, RoleID)");
            userCommand.Parameters.AddWithValue("@Username", userDTO.Name);
            userCommand.Parameters.AddWithValue("@Password", userDTO.Password);
            userCommand.Parameters.AddWithValue("@RoleId", userDTO.Role.Id);
            dal.UploadData(userCommand);
        }

        public void CreateRole(RoleDTO roleDTO)
        {
            SqlCommand roleCommand = new("INSERT INTO Role(RoleName) VALUES(@RoleName)");
            roleCommand.Parameters.AddWithValue("@RoleName", roleDTO.Name);
        }
    }
}
