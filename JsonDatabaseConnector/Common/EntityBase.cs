using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace JsonDatabaseConnector.Common
{
    [DataContract]
    public  class EntityBase
    {
        [DataMember]
        public int Id { get; set; }
    }
}
