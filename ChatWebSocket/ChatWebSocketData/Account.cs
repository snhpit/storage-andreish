using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ChatWebSocketData
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public string Name { get; set; }
    }
}
