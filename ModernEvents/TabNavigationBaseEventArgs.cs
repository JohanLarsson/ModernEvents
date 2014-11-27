namespace ModernEventsBox
{
    using System;

    /// <summary>
    /// Defines the base navigation event arguments.
    /// </summary>
    public abstract class TabNavigationBaseEventArgs : EventArgs
    {
        protected TabNavigationBaseEventArgs(NavigatingTab tab)
        {
            Tab = tab;
        }
        
        /// <summary>
        /// Gets the tab  that raised this event.
        /// </summary>
        public NavigatingTab Tab { get; private set; }
    }
}