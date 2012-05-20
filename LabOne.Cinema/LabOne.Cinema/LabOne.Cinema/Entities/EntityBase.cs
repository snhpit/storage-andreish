using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    [Serializable]
    public class EntityBase
    {
        public string ID { get; set; } //protected set

        protected EntityBase()
        {
            ID = Guid.NewGuid().ToString();
        }
    }
}