using System;

namespace RustyDragonBasesAndInterfaces.BusinessLogics
{
    public interface IDaysManager
    {
        event DaysManagerEventHandler OnNextDay;

        DateTime Now { get; }

        void Start();

        void Stop();
    }

    public delegate void DaysManagerEventHandler(object sender, DaysManagerEventArgs e);

    public class DaysManagerEventArgs : EventArgs
    {
        public readonly DateTime Now;
        public readonly int DayNumber;

        public DaysManagerEventArgs(DateTime now, int dayNumber)
        {
            Now = now;
            DayNumber = dayNumber;
        }
    }
}