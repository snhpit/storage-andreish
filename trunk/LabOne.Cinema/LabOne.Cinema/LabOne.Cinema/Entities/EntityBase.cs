using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    public class EntityBase
    {
        public string ID { get; set; } //protected set

        public EntityBase()
        {
            ID = Guid.NewGuid().ToString();
        }
    }
}