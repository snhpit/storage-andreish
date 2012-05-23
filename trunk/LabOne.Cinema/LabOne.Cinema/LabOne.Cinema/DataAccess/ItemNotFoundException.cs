using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LabOne.Cinema.DataAccess
{
    [Serializable]
    public class ItemNotFoundException : Exception
    {
        private string _message;

        public ItemNotFoundException()
        {
        }

        public ItemNotFoundException(string message)
            : base(message)
        {
            _message = message;
        }

        public ItemNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
            _message = message;
        }

        protected ItemNotFoundException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        public override string Message
        {
            get { return string.Format("Item not found.\n{0}", _message); }
        }
    }
}