﻿using System;
using System.Runtime.Serialization;

namespace FootballManager.Domain.Exceptions
{
    [Serializable]
    public class PlayerAlreadyInTeamException : Exception
    {
        public PlayerAlreadyInTeamException()
        {
        }

    }
}