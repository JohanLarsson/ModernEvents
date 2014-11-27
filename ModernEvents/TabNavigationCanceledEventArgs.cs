namespace ModernEventsBox
{
    using System;

    public class TabNavigationCanceledEventArgs : TabNavigationBaseEventArgs
    {
        public TabNavigationCanceledEventArgs(NavigatingTab tab, Uri navigatingTo) : base(tab)
        {
            NavigatingTo = navigatingTo;
        }

        /// <summary>
        /// Gets the NavigatingTo uri for the target being navigated to.
        /// </summary>
        public Uri NavigatingTo { get; private set; }

        public bool Cancel { get; set; }
    }
}