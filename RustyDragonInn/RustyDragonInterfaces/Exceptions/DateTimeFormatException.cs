using System;

namespace RustyDragonBasesAndInterfaces.Exceptions
{
    /// <summary>
    /// a new exception for the wrong date and time formate.
    /// </summary>
    [Serializable]
    public class DateTimeFormatException : Exception
    {
        public DateTimeFormatException(string message) : base(message)
        {
        }
    }
}