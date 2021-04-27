using System;
using System.Runtime.Serialization;

namespace FootballManager.Domain.Entities
{
    [Serializable]
    internal class PlayerFromAnotherTeamException : Exception
    {
        public PlayerFromAnotherTeamException()
        {
        }

        public PlayerFromAnotherTeamException(string message) : base(message)
        {
        }

        public PlayerFromAnotherTeamException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PlayerFromAnotherTeamException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}