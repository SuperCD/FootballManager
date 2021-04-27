using System;
using System.Runtime.Serialization;

namespace FootballManager.Domain.Entities
{
    [Serializable]
    public class FormationSlotIncompatibleException : Exception
    {
        public FormationSlotIncompatibleException()
        {
        }

        public FormationSlotIncompatibleException(string message) : base(message)
        {
        }

        public FormationSlotIncompatibleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FormationSlotIncompatibleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}