using System;
using System.Runtime.Serialization;

namespace FootballManager.Domain.Entities
{
    [Serializable]
    internal class PlayerUnavailableException : Exception
    {
        public PlayerUnavailableException()
        {
        }

        public PlayerUnavailableException(string message) : base(message)
        {
        }

        public PlayerUnavailableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PlayerUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}