namespace ModernEventsBox
{
    using System;

    public class TabNavigationEventArgs : TabNavigationBaseEventArgs
    {
        public TabNavigationEventArgs(NavigatingTab tab, Uri navigatedFrom, Uri navigatedTo) 
            : base(tab)
        {
            NavigatedFrom = navigatedFrom;
            NavigatedTo = navigatedTo;
        }
      
        /// <summary>
        /// Gets the NavigatedFrom uri for the target being navigated to.
        /// </summary>
        public Uri NavigatedFrom { get; private set; }
       
        /// <summary>
        /// Gets the NavigatedTo uri for the target being navigated to.
        /// </summary>
        public Uri NavigatedTo { get; private set; }
    }
}