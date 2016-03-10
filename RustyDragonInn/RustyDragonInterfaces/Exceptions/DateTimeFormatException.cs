using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustyDragonBasesAndInterfaces.Exceptions
{
    /// <summary>
    /// a new exception for the wrong date and time formate.
    /// </summary>
    public class DateTimeFormatException : Exception
    {
        public DateTimeFormatException(string message):base(message)
        {
            
        }
    }
}
