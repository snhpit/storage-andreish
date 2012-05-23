using System;
using System.Runtime.Serialization;

namespace LabOne.Cinema.DataAccess
{
    [Serializable]
    public class TypeIsNotEnumerableException : Exception
    {
        private readonly string _message;

        public TypeIsNotEnumerableException()
        {
        }

        public TypeIsNotEnumerableException(string message)
            : base(message)
        {
            _message = message;
        }

        public TypeIsNotEnumerableException(string message, Exception inner)
            : base(message, inner)
        {
            _message = message;
        }

        protected TypeIsNotEnumerableException(
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