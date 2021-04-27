using System;
using System.Runtime.Serialization;

namespace FootballManager.Domain.Exceptions
{
    [Serializable]
    public class UnknownFormationTypeException : Exception
    {
        public UnknownFormationTypeException()
        {
        }

    }
}