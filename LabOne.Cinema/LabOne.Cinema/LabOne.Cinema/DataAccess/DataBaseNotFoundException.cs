using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LabOne.Cinema.DataAccess
{
    [Serializable]
    public class DataBaseNotFoundException : Exception
    {
        private readonly string _fileExtension;
        private readonly string _message;

        public DataBaseNotFoundException(string fileExtension)
        {
            _fileExtension = fileExtension;
        }

        public DataBaseNotFoundException(string message, string fileExtension)
            : base(message)
        {
            _message = message;
            _fileExtension = fileExtension;
        }

        public DataBaseNotFoundException(string message, Exception inner, string fileExtension)
            : base(message, inner)
        {
            _message = message;
            _fileExtension = fileExtension;
        }

        protected DataBaseNotFoundException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        public override string Message
        {
            get { return string.Format("{0}\nData base with this .{1} extension not found", _message, _fileExtension); }
        }
    }
}