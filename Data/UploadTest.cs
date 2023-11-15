using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UploadTest
    {
        public void TestUpload()
        {
            ParameterDTO parameter1 = new();
            ParameterDTO parameter2 = new();
            ParameterDTO parameter3 = new();
            ParameterDTO parameter4 = new();
            EventDTO event1 = new();
            EventDTO event2 = new();
            MachineDTO testmachine = new();
            testmachine.MachineName = "MachineTestTest";
            event1.EventName = "TestName1";
            event2.EventName = "TestName2";
            event1.EventSourceID = "TestSource1";
            event2.EventSourceID = "TestSource2";
            parameter1.ParameterName = "TestName1";
            parameter2.ParameterName = "TestName2";
            parameter3.ParameterName = "TestName3";
            parameter4.ParameterName = "TestName4";
            parameter1.ParameterSourceID = "TestSource1";
            parameter2.ParameterSourceID = "TestSource2";
            parameter3.ParameterSourceID = "TestSource3";
            parameter4.ParameterSourceID = "TestSource4";
            event1.Parameters.Add(parameter1);
            event1.Parameters.Add(parameter2);
            event2.Parameters.Add(parameter3);
            event2.Parameters.Add(parameter4);
            testmachine.Events.Add(event1);
            testmachine.Events.Add(event2);
            Upload upload = new();
            upload.UploadMachine(testmachine);
        }
    }
}
