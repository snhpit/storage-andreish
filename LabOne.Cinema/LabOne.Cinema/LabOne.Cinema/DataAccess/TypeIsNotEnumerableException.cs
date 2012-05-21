using System;
using System.Runtime.Serialization;

namespace LabOne.Cinema.DataAccess
{
    [Serializable]
    public class IsValueTypeException : Exception
    {
        private readonly string _message;

        public IsValueTypeException()
        {
        }

        public IsValueTypeException(string message)
            : base(message)
        {
            _message = message;
        }

        public IsValueTypeException(string message, Exception inner)
            : base(message, inner)
        {
            _message = message;
        }

        protected IsValueTypeException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        public override string Message
        {
            get { return string.Format("{0}\nIncorrect data. Need enumerable.", _message); }
        }
    }
}