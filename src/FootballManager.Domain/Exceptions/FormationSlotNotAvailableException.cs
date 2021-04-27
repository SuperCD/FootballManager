using System;
using System.Runtime.Serialization;

namespace FootballManager.Domain.Entities
{
    [Serializable]
    public class FormationSlotNotAvailableException : Exception
    {
        public FormationSlotNotAvailableException()
        {
        }

        public FormationSlotNotAvailableException(string message) : base(message)
        {
        }

        public FormationSlotNotAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FormationSlotNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}