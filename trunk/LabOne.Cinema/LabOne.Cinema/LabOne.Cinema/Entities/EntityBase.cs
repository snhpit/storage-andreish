using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabOne.Cinema.Entities
{
    [Serializable]
    public abstract class EntityBase
    {
        public string ID { get; set; }

        protected EntityBase()
        {
            ID = Guid.NewGuid().ToString();
        }
    }
}