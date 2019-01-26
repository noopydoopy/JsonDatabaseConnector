using JsonDatabaseConnector.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ConsoleApp1.DataContracts
{
    [DataContract]
    public class Person : EntityBase
    {
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
