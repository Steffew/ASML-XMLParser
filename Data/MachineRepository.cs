using DAL.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MachineRepository
    {
        /* public MachineDTO LoadMachineByName(string machineName)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Machine WHERE MachineName = '" + machineName + "';", sqlConnection);
            MachineDTO machineDTO = new MachineDTO();

            sqlConnection.Open();
            SqlDataReader DataReader = command.ExecuteReader();
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    machineDTO.MachineID = DataReader.GetInt32(0);
                    machineDTO.MachineName = DataReader.GetString(1);
                }
            }
            DataReader.Close();
            sqlConnection.Close();
            return machineDTO; 
        } */
    }
}
