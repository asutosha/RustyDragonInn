using System;
using System.Timers;
using RustyDragonBasesAndInterfaces.BusinessLogics;

namespace RustyDragonInn.BusinessLogics
{
    /// <summary>
    /// Days Manager takes the control of calculating next day and notifying 
    /// the subscriber(s) of OnNextDay event that a new day has come
    /// </summary>
    public class DaysManager : IDaysManager
    {
        public event DaysManagerEventHandler OnNextDay;
        private readonly Timer _internalTimer ;
        private int _dayCounter = 1;
        public DateTime Now { get; private set; }

        public DaysManager(int interval,DateTime now)
        {
            Now = now;

            _internalTimer = new Timer(interval);
            _internalTimer.Elapsed += _internalTimer_Elapsed;
            _internalTimer.AutoReset = true;
            _internalTimer.Enabled = true;
            Stop();
        }

        public void Start()
        {
            _internalTimer.Start();
        }

        public void Stop()
        {
            _dayCounter = 1;
            _internalTimer.Stop();
        }
        private void _internalTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _dayCounter++;
            Now =Now.AddDays(+1);
            var eventArgs=new DaysManagerEventArgs(Now, _dayCounter);
            OnNextDay?.Invoke(this, eventArgs);
        }
    }

}
