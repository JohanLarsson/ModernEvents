namespace ModernEvents
{
    using System;
    using System.Windows.Forms.VisualStyles;

    /// <summary>
    /// Defines the base navigation event arguments.
    /// 
    /// </summary>
    public abstract class TabNavigationBaseEventArgs : EventArgs
    {
        protected TabNavigationBaseEventArgs(NavigatingTab tab)
        {
            Tab = tab;
        }
        
        /// <summary>
        /// Gets the tab  that raised this event.
        /// 
        /// </summary>
        public NavigatingTab Tab { get; private set; }
    }

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

    public class TabNavigatedEventArgs : TabNavigationBaseEventArgs
    {
        public TabNavigatedEventArgs(NavigatingTab tab, Uri navigatedFrom, Uri navigatedTo) 
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