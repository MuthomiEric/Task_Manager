using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class NullDbContextException : Exception
    {
        public NullDbContextException(string message="Null db context") : base(message) { }
        protected NullDbContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
